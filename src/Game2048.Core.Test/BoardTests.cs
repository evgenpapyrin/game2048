using Game2048.Core.Generators;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Game2048.Core.Test
{
    public partial class BoardTests
    {
        [Fact]
        public void CreateBoardSuccessTest()
        {
            int sizeBoard = 4;

            Board board = Board.CreateBoard(new NextGenerator(), sizeBoard);

            var tiles = board.GetTiles().ToList();

            Assert.Equal(sizeBoard * sizeBoard, tiles.Count);

            var notEmptyTiles = board.GetTiles().Where(t => t.Value != 0).ToList();
            int startTilesInitCount = 2;
            Assert.Equal(startTilesInitCount, notEmptyTiles.Count);

            foreach (var tile in notEmptyTiles)
            {
                AssertInitTile(tile);
            }
        }

        [Fact]
        public void RestoreBoardSuccessTest()
        {
            int sizeBoard = 4;

            Board board = Board.CreateBoard(new NextGenerator(), sizeBoard);

            board.MoveDown();
            board.MoveLeft();

            List<Tile> tiles = board.GetTiles().ToList();

            Board restoreBoard = Board.RestoreBoard(new NextGenerator(), board.CurrentScore, sizeBoard, board.GetTiles().ToArray());
            
            AssertTiles(tiles, restoreBoard.GetTiles());
        }
      
        [Fact]
        public void NextNotAvailableStepTest()
        {
            int[,] values = new int[,]
            {
                { 2, 4, 2, 4},
                { 4, 2, 4, 2},
                { 2, 4, 2, 4},
                { 4, 2, 4, 2}
            };

            Board board = Board
                .RestoreBoard(new NextGenerator(), 0, values.GetLength(0), ConvertToTiles(values));

            Assert.False(board.NextStepAvailable());
        }

        [Fact]
        public void NextAvailableStepEmptyTileTest()
        {
            int[,] values = new int[,]
            {
                { 2, 4, 2, 4},
                { 4, 2, 4, 2},
                { 2, 0, 2, 4},
                { 4, 2, 4, 2}
            };

            Board board = Board
                .RestoreBoard(new NextGenerator(), 0, values.GetLength(0), ConvertToTiles(values));

            Assert.True(board.NextStepAvailable());
        }

        [Fact]
        public void NextAvailableStepEqualTileTest()
        {
            int[,] values = new int[,]
            {
                { 2, 4, 2, 4},
                { 4, 2, 4, 2},
                { 2, 8, 8, 4},
                { 4, 2, 4, 2}
            };

            Board board = Board
                .RestoreBoard(new NextGenerator(), 0, values.GetLength(0), ConvertToTiles(values));

            Assert.True(board.NextStepAvailable());
        }

        #region Private methods

        private Tile[,] GetEmptyTiles2DArray(int size = 4)
        {
            Tile[,] result = new Tile[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = new Tile(i, j);
                }   
            }

            return result;
        }

        private Tile[] ConvertToArray(Tile[,] tiles)
        {
            List<Tile> result = new List<Tile>();

            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    result.Add(tiles[i, j]);
                }
            }

            return result.ToArray();
        }

        private Tile[] ConvertToTiles(int[,] values)
        {
            List<Tile> result = new List<Tile>();

            for (int i = 0; i < values.GetLength(0); i++)
            {
                for (int j = 0; j < values.GetLength(1); j++)
                {
                    result.Add(new Tile(i, j) { Value = values[i, j] });
                }
            }

            return result.ToArray();
        }

        private int[,] ConvertToValuesArray(Tile[] tiles)
        {
            int[,] result = new int[tiles.Max(t => t.X) + 1, tiles.Max(t => t.Y) + 1];

            foreach (var tile in tiles)
            {
                result[tile.X, tile.Y] = tile.Value;
            }

            return result;
        }

        private INextGenerator BuildMockGenerator(Tile newTile)
        {
            Mock<INextGenerator> mockGenerator = new Mock<INextGenerator>();

            mockGenerator
                .Setup(g => g.NextPosition(It.IsAny<List<Tile>>()))
                .Returns((newTile.X, newTile.Y));

            mockGenerator
                .Setup(g => g.NextValue())
                .Returns(newTile.Value);

            return mockGenerator.Object;
        }
             
        #endregion

        #region Assert help

        private void AssertInitTile(Tile tile)
        {
            Assert.True(tile.Value == 2 || tile.Value == 4);
        }

        private void AssertTiles(IEnumerable<Tile> expectedTiles, IEnumerable<Tile> actualTiles)
        {
            Assert.Equal(expectedTiles.Count(), actualTiles.Count());

            foreach (var expectedTile in expectedTiles)
            {
                var actualTile = actualTiles.FirstOrDefault(t => 
                    t.X == expectedTile.X && 
                    t.Y == expectedTile.Y &&
                    t.Value == expectedTile.Value);

                Assert.NotNull(actualTile);
            }
        }

        private void AssertMove(
            int[,] valuesBeforeAction, 
            int[,] valuesAfterAction, 
            int expectedScore, 
            Action<Board> moveAction, 
            Tile newTile)
        {
            Board board = Board
               .RestoreBoard(
                   BuildMockGenerator(newTile),
                   0,
                   valuesBeforeAction.GetLength(0),
                   ConvertToTiles(valuesBeforeAction));

            moveAction(board);

            AssertValues(valuesAfterAction, ConvertToValuesArray(board.GetTiles().ToArray()));
        }

        private void AssertValues(int[,] expectedValues, int[,] actualValue)
        {
            Assert.Equal(expectedValues.GetLength(0), actualValue.GetLength(0));
            Assert.Equal(expectedValues.GetLength(1), actualValue.GetLength(1));

            for (int i = 0; i < expectedValues.GetLength(0); i++)
            {
                for (int j = 0; j < expectedValues.GetLength(1); j++)
                {
                    Assert.Equal(expectedValues[i, j], actualValue[i, j]);
                }
            }
        }

        #endregion
    }
}
