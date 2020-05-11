using Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseLogic
{
    public class Export
    {
        private static string _connString = @"Data Source=LAPTOP-DPRRU9CI\SQLEXPRESS1;Initial Catalog=EscapeFromTheWoods;Integrated Security=True";
        public void ExportToDB(List<Monkey> monkeys)
        {
            var woods = monkeys.Select(m => m.Wood).Distinct();
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Run(() => ExportToWoodRecords(woods.ToList())));
            tasks.Add(Task.Run(() => ExportToMonkeyRecords(monkeys)));
            tasks.Add(Task.Run(() => ExportToLogs(monkeys)));
            Task.WaitAll(tasks.ToArray());

        }
        private async Task ExportToWoodRecords(List<Wood> woods)
        {
            Console.WriteLine($"Start - write to DB WoodRecords");
            using (SqlConnection c = new SqlConnection(_connString))
            {
                c.Open();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(c))
                {
                    int recordIdCounter = 1;
                    DataTable woodRecords = new DataTable();
                    woodRecords.Columns.Add("recordId", typeof(int));
                    woodRecords.Columns.Add("woodID", typeof(int));
                    woodRecords.Columns.Add("treeID", typeof(int));
                    woodRecords.Columns.Add("x", typeof(int));
                    woodRecords.Columns.Add("y", typeof(int));

                    foreach (var w in woods)
                    {
                        foreach (var tree in w.Trees)
                        {
                            woodRecords.Rows.Add(recordIdCounter++, w.Id, tree.Id, tree.X, tree.Y);
                        }
                    }
                    bulkCopy.DestinationTableName = "WoodRecords";
                    bulkCopy.ColumnMappings.Add("recordId", "recordId");
                    bulkCopy.ColumnMappings.Add("woodID", "woodID");
                    bulkCopy.ColumnMappings.Add("treeID", "treeID");
                    bulkCopy.ColumnMappings.Add("x", "x");
                    bulkCopy.ColumnMappings.Add("y", "y");
                    bulkCopy.WriteToServer(woodRecords);

                }
            }
            Console.WriteLine($"Stop - write to DB WoodRecords");

        }
        private async Task ExportToMonkeyRecords(List<Monkey> monkeys)
        {
            Console.WriteLine($"Start - write to DB MonkeyRecords");
            using (SqlConnection c = new SqlConnection(_connString))
            {
                c.Open();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(c))
                {
                    int recordIdCounter = 1;
                    DataTable monkeyRecords = new DataTable();
                    monkeyRecords.Columns.Add("recordId", typeof(int));
                    monkeyRecords.Columns.Add("monkeyId", typeof(int));
                    monkeyRecords.Columns.Add("monkeyName", typeof(string));
                    monkeyRecords.Columns.Add("woodId", typeof(int));
                    monkeyRecords.Columns.Add("seqnr", typeof(int));
                    monkeyRecords.Columns.Add("treeID", typeof(int));
                    monkeyRecords.Columns.Add("x", typeof(int));
                    monkeyRecords.Columns.Add("y", typeof(int));

                    foreach (var m in monkeys)
                    {
                        int seqnr = 1;
                        foreach (var t in m.VisitedTrees)
                        {
                            if (t != null)
                                monkeyRecords.Rows.Add(recordIdCounter++, m.Id, m.Name, m.Wood.Id, seqnr++, t.Id, t.X, t.Y);
                        }

                    }

                    bulkCopy.DestinationTableName = "MonkeyRecords";
                    bulkCopy.ColumnMappings.Add("recordId", "recordId");
                    bulkCopy.ColumnMappings.Add("monkeyId", "monkeyId");
                    bulkCopy.ColumnMappings.Add("monkeyName", "monkeyName");
                    bulkCopy.ColumnMappings.Add("woodId", "woodId");
                    bulkCopy.ColumnMappings.Add("seqnr", "seqnr");
                    bulkCopy.ColumnMappings.Add("treeID", "treeID");
                    bulkCopy.ColumnMappings.Add("x", "x");
                    bulkCopy.ColumnMappings.Add("y", "y");
                    bulkCopy.WriteToServer(monkeyRecords);

                }
            }
            Console.WriteLine($"Stop - write to DB MonkeyRecords");
        }
        private async Task ExportToLogs(List<Monkey> monkeys)
        {
            Console.WriteLine($"Start - write to DB Logs");
            using (SqlConnection c = new SqlConnection(_connString))
            {
                c.Open();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(c))
                {
                    DataTable logs = new DataTable();
                    logs.Columns.Add("Id", typeof(int));
                    logs.Columns.Add("woodID", typeof(int));
                    logs.Columns.Add("monkeyID", typeof(int));
                    logs.Columns.Add("message", typeof(string));

                    int logIdCounter = 1;
                    foreach (var m in monkeys)
                    {
                        foreach (var t in m.VisitedTrees)
                        {
                            if (t != null)
                                logs.Rows.Add(logIdCounter++, m.Wood.Id, m.Id, $"{m.Name} is now in tree {t.Id} at location ({t.X}, {t.Y})");
                        }
                        logs.Rows.Add(logIdCounter++, m.Wood.Id, m.Id, $"{m.Name} has left the woods");
                    }
                    bulkCopy.DestinationTableName = "Logs";
                    bulkCopy.ColumnMappings.Add("Id", "Id");
                    bulkCopy.ColumnMappings.Add("woodID", "woodID");
                    bulkCopy.ColumnMappings.Add("monkeyID", "monkeyID");
                    bulkCopy.ColumnMappings.Add("message", "message");
                    bulkCopy.WriteToServer(logs);

                }
            }
            Console.WriteLine($"Stop - write to DB Logs");
        }
    }
}
