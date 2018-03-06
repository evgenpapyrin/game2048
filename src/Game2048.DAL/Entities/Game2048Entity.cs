using Game2048.DAL.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Game2048.DAL.Entities
{
    public class Game2048Entity
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public int Score { get; set; }

        public int SizeBoard { get; set; }

        [NotMapped]
        public List<TileEntity> Boards { get; set; } = new List<TileEntity>();
        
        public string BoardXml
        {
            get => XmlHelper.XmlSerializeToString(Boards);
            set => Boards = XmlHelper.XmlDeserializeFromString<List<TileEntity>>(value);
        }
    }
}
