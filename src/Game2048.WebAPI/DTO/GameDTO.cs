using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game2048.WebAPI.DTO
{
    public class GameDTO
    {
        public int ID { get; set; }

        public int UserID { get; set; }
        
        public TileDTO[] Board { get; set; }

        public int Score { get; set; }

        public bool NextStepAvailable { get; set; }

        public string StatusGame { get; set; }
    }
}
