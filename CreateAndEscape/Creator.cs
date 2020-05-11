using Contracts;
using System;
using System.Collections.Generic;

namespace CreateAndEscape
{
    public class Creator
    {
        
        public Wood CreateWood(int maxX, int maxY, int numberOfTrees, int id)
        {
            var w = new Wood(maxX, maxY, id);

            var rnd = new Random();
            w.Trees = new List<Tree>();
            int treeId = 1;
            while (w.Trees.Count < numberOfTrees)
            {
                var t = new Tree(rnd.Next(1, maxX), rnd.Next(1, maxY), treeId++);
                if (!w.Trees.Contains(t))
                {
                    w.Trees.Add(t);
                }
                else
                    treeId--;
            }
            return w;
        }
        public List<Monkey> CreateAndPlaceMonkeysInTheWood(int numberOfMonkeys, Wood w)
        {
            List<Tree> freeTrees = new List<Tree>(w.Trees);
            List<Monkey> placedMonkeys = new List<Monkey>();
            var rnd = new Random();

            while (placedMonkeys.Count < numberOfMonkeys)
            {
                var m = new Monkey(placedMonkeys.Count, $"Monkey {placedMonkeys.Count}", w);
                int treeIndex = rnd.Next(freeTrees.Count);
                m.VisitedTrees.Add(freeTrees[treeIndex]);
                placedMonkeys.Add(m);
                freeTrees.RemoveAt(treeIndex);
            }
            return placedMonkeys;

        }

    }
}
