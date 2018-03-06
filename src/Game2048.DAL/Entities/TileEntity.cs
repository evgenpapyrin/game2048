using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Game2048.DAL.Entities
{
    [NotMapped]
    public class TileEntity
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Value { get; set; }
    }
}
