using Game2048.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game2048.DAL
{
    public class GameContext
        : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options)
            : base(options)
        {
            
        }

        public DbSet<Game2048Entity> Games { get; set; }
    }
}
