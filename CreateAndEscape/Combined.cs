using Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CreateAndEscape
{
    public class Combined
    {
        public List<Monkey> CreateWoodsAndPlaceMonkeysInsideAndEscapeAsync(int woodId, int numberOfMonkeysInWood, int numberOfTreesPerWood, int width, int height)
        {
            var monkeys = new List<Monkey>();
            var c = new Creator();
            var w = c.CreateWood(width, height, numberOfTreesPerWood, woodId);
            monkeys.AddRange(c.CreateAndPlaceMonkeysInTheWood(numberOfMonkeysInWood, w));

            var esc = new Escaper();
            List<Thread> threads = new List<Thread>();
            foreach (var m in monkeys)
            {
                ThreadStart ts = new ThreadStart(() => esc.Escape(m));
                Thread t = new Thread(ts);
                t.Start();
                threads.Add(t);
            }
            foreach (var t in threads)
            {
                t.Join();
            }
            return monkeys;
        }
    }
}
