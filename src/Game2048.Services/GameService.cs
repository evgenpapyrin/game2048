using Game2048.Core;
using Game2048.DAL;
using Game2048.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2048.Services
{
    public class GameService
    {
        private GameContext _context;

        private INextGenerator _nextGenerator;

        public GameService(GameContext context, INextGenerator nextGenerator)
        {
            _context = context;
            _nextGenerator = nextGenerator;

            context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
        }
        
        public Game GetGame(int gameID)
        {
            Game2048Entity gameEntity = _context.Games.FirstOrDefault(g => g.ID == gameID);

            if(gameEntity == null)
            {
                return null;
            }

            Board board = Board.RestoreBoard(
                _nextGenerator, 
                gameEntity.Score, 
                gameEntity.SizeBoard, 
                gameEntity.Boards.Select(ToTile).ToArray());

            return new Game(gameEntity.ID, gameEntity.UserID, board);
        }

        public void SaveGame(Game game)
        {
            Game2048Entity gameEntity = new Game2048Entity()
            {
                ID = game.ID,
                Score = game.Score,
                SizeBoard = game.Board.Size,
                UserID = game.UserID,
                Boards = game.Board.GetTiles().Select(ToTileEntity).ToList()
            };

            if(gameEntity.ID == 0)
            {
                _context.Games.Add(gameEntity);
            }
            else
            {
                _context.Games.Update(gameEntity);
            }

            _context.SaveChanges();

            game.ID = gameEntity.ID;
        }

        public Game CreateGame(int userID)
        {
            Board board = Board.CreateBoard(_nextGenerator);

            return new Game(0, userID, board);
        }

        #region Mappers

        private Tile ToTile(TileEntity tileEntity)
        {
            return new Tile(tileEntity.X, tileEntity.Y)
            {
                 Value = tileEntity.Value
            };
        }

        private TileEntity ToTileEntity(Tile tile)
        {
            return new TileEntity()
            {
                X = tile.X,
                Y = tile.Y,
                Value = tile.Value
            };
        }

        #endregion
    }
}
