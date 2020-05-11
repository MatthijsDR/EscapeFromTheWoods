using Contracts;
using CreateAndEscape;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Async
{
    public static class AsyncExecution
    {
        private static async Task<List<Monkey>> CreateAndExportToFilesOneWoodAscync(int woodId, int numberOfMonkeys, int numberOfTrees, int width, int height)
        {
            var combo = new Combined();
            var m = combo.CreateWoodsAndPlaceMonkeysInsideAndEscapeAsync(woodId, numberOfMonkeys, numberOfTrees, width, height);
            List<Task> tasks = new List<Task>();
            tasks.Add(Task.Run(() => ExportToFiles.Export.ExportToBitMap(m)));
            tasks.Add(Task.Run(() => ExportToFiles.Export.ExportToTextFile(m)));
            Task.WaitAll(tasks.ToArray());
            return m;
        }
        public static async void CreateAndExportFilesAndDBWoodsAsync(int numberOfWoods, int numberOfMonkeys, int numberOfTrees, int width, int height)
        {
            List<Task<List<Monkey>>> tasks = new List<Task<List<Monkey>>>();
            List<Monkey> monkeys = new List<Monkey>();
            for (int i = 0; i < numberOfWoods; i++)
            {
                tasks.Add(CreateAndExportToFilesOneWoodAscync(i, numberOfMonkeys, numberOfTrees, width, height));
            }
            var listListMonkeys = await Task.WhenAll(tasks.ToArray());
            foreach (var monkeyList in listListMonkeys)
            {
                foreach (var monkey in monkeyList)
                {
                    monkeys.Add(monkey);
                }
            }
            var exp = new DatabaseLogic.Export();
            exp.ExportToDB(monkeys);
        }
    }
}
