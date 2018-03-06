using System;
using System.Collections.Generic;
using System.Text;

namespace Game2048.Core
{
    /// <summary>
    /// Представляет плитку игру
    /// </summary>
    public class Tile
    {
        public int X { get; private set; }

        public int Y { get; private set; }

        public int Value { get; set; }

        public Tile(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Tile Copy()
        {
            return new Tile(X, Y)
            {
                Value = this.Value
            };
        }
    }
}
