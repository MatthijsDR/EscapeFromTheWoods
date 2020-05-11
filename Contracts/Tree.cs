using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public class Tree
    {
        public Tree(int x, int y, int id)
        {
            X = x;
            Y = y;
            Id = id;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Id { get; set; }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Tree p = (Tree)obj;
                return (this.X == p.X) && (this.Y == p.Y);
            }
        }

        public override int GetHashCode()
        {
            return (this.X << 2) ^ this.Y;
        }

    }
}
