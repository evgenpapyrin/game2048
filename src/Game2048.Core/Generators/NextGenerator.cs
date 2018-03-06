using System;
using System.Collections.Generic;
using System.Text;

namespace Game2048.Core.Generators
{
    /// <summary>
    /// Преставляет генератор новых значений для игры
    /// </summary>
    public class NextGenerator
        : INextGenerator
    {
        private static Random _random = new Random();


        public int NextValue()
        {
            return (_random.Next(100) % 10) != 0 ? (sbyte)2 : (sbyte)4;// 90% - 2, 10% - 4
        }

        public (int x, int y) NextPosition(List<Tile> emptyTiles)
        {
            var nextTile = emptyTiles[_random.Next(emptyTiles.Count)];

            return (nextTile.X, nextTile.Y);
        }
    }
}
