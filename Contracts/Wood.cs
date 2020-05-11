using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public class Wood
    {

        public Wood(int maxX, int maxY, int id)
        {
            MaxX = maxX;
            MaxY = maxY;
            Id = id;
        }
        public int Id { get; set; }
        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public List<Tree> Trees { get; set; }
    }
}
