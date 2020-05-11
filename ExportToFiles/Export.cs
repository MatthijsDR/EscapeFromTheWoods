using Contracts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExportToFiles
{
    public static class Export
    {
        public static void ExportToTextFile(List<Monkey> monkeys)
        {
            Console.WriteLine($"Start writing to text-file wood {monkeys[0].Wood.Id}");
            using (FileStream fs = new FileStream(Path.Combine(Config.Path, monkeys[0].Wood.Id.ToString() + "_escapeRoutesLog.txt"), FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    var tempMonkeys = new HashSet<Monkey>(monkeys);
                    tempMonkeys.OrderBy(m => m.Name);
                    int incrementer = 0;
                    StringBuilder sb = new StringBuilder();
                    while (tempMonkeys.Count != 0)
                    {
                        List<Monkey> monkeyToBeDeleted = new List<Monkey>();
                        foreach (var m in tempMonkeys)
                        {
                            var currentTree = m.VisitedTrees[incrementer];
                            if (currentTree != null)
                            {
                                sb.Append($"{m.Name} is in tree {currentTree.Id} at ({currentTree.X},{currentTree.Y})\n");
                            }
                            else
                            {
                                monkeyToBeDeleted.Add(m);
                            }
                        }
                        foreach (var m in monkeyToBeDeleted)
                        {
                            tempMonkeys.Remove(m);
                        }
                        incrementer++;
                    }

                    sw.Write(sb);
                    Console.WriteLine($"End writing to text-file wood {monkeys[0].Wood.Id}");
                }
            }

        }

        public static async Task ExportToBitMap(List<Monkey> monkeysInTheSameWood)
        {
            int _drawingFactor = 10;

            var wood = monkeysInTheSameWood[0].Wood;
            Console.WriteLine($"Start writing to jpg-file wood {wood.Id}");
            var bm = new Bitmap(wood.MaxX * _drawingFactor, wood.MaxY * _drawingFactor);

            Graphics g = Graphics.FromImage(bm);

            var greenPen = new Pen(Color.Green, 1);
            foreach (var tree in wood.Trees)
            {
                g.DrawEllipse(greenPen, getRectangleAroundPoint(tree));
            }
            var rnd = new Random();
            foreach (var m in monkeysInTheSameWood)
            {
                Brush b = new SolidBrush(Color.FromArgb(rnd.Next(255), rnd.Next(255), rnd.Next(255)));
                var firstTree = m.VisitedTrees[0];
                g.FillEllipse(b, getRectangleAroundPoint(firstTree));

                var rndPen = new Pen(b);
                for (int i = 0; i < m.VisitedTrees.Count - 2; i++)
                {
                    g.DrawLine(rndPen, m.VisitedTrees[i].X * _drawingFactor, m.VisitedTrees[i].Y * _drawingFactor, m.VisitedTrees[i + 1].X * _drawingFactor, m.VisitedTrees[i + 1].Y * _drawingFactor);
                }
            }
            bm.Save(Path.Combine(Config.Path, wood.Id.ToString() + "_escapeRoutes.jpg"), ImageFormat.Jpeg);
            Console.WriteLine($"End writing to jpg-file wood {wood.Id}");

            Rectangle getRectangleAroundPoint(Tree tree)
            {
                return new Rectangle(tree.X * _drawingFactor - _drawingFactor / 2, tree.Y * _drawingFactor - _drawingFactor / 2, _drawingFactor, _drawingFactor);
            }
        }


    }
}
