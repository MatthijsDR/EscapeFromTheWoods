using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CreateAndEscape
{
    public class Escaper
    {
        public void Escape(Monkey m)
        {
            Console.WriteLine($"Start calculating escaperoute for wood: {m.Wood.Id} , monkey: {m.Name}");
            while (m.VisitedTrees[m.VisitedTrees.Count - 1] != null)
            {
                m.VisitedTrees.Add(ClosestUnenteredTree(m));
            }
            Console.WriteLine($"end calculating escape route for wood: {m.Wood.Id} , monkey: {m.Name}");

        }
        private static int DistanceToBorder(Tree t, Wood w)
        {
            List<int> distances = new List<int>() { t.X, t.Y, w.MaxX - t.X, w.MaxY - t.Y };
            return distances.Min();
        }
        private static double DistanceBetweenTrees(Tree t1, Tree t2)
        {
            return Math.Sqrt(Math.Pow(t1.X - t2.X, 2) + Math.Pow(t1.Y - t2.Y, 2));
        }
        private static Tree ClosestUnenteredTree(Monkey m)
        {
            List<Tree> possibleTrees = new List<Tree>(m.Wood.Trees);
            foreach (var tree in m.VisitedTrees)
            {
                possibleTrees.Remove(tree);
            }
            Tree closestTree = null;
            var smallestDistance = (double)DistanceToBorder(m.VisitedTrees[m.VisitedTrees.Count - 1], m.Wood);
            for (int i = 1; i < possibleTrees.Count; i++)
            {
                var distance = DistanceBetweenTrees(possibleTrees[i], m.VisitedTrees[m.VisitedTrees.Count - 1]);
                if (smallestDistance > distance)
                {
                    smallestDistance = distance;
                    closestTree = possibleTrees[i];
                }
            }
            return closestTree;

        }
    }
}
