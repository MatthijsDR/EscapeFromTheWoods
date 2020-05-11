using System;
using System.Collections.Generic;

namespace Contracts
{
    public class Monkey
    {
        public Monkey(int id, string name, Wood wood)
        {
            Id = id;
            Name = name;
            VisitedTrees = new List<Tree>();
            Wood = wood;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public List<Tree> VisitedTrees { get; set; }
        public Wood Wood { get; set; }
    }
}
