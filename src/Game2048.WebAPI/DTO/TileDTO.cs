using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game2048.WebAPI.DTO
{
    public class TileDTO
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Value { get; set; }
    }
}
