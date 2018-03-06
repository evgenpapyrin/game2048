using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Game2048.Core.Test
{
    public partial class BoardTests
    {
        [Fact]
        public void DownTest_1()
        {
            int[,] valuesBeforeMove = new int[,]
            {
               { 2, 0, 0, 0},

               { 0, 0, 0, 0},

               { 0, 0, 0, 0},

               { 0, 0, 0, 0}
            };


            int[,] valuesAfterMove = new int[,]
            {
              { 0, 0, 0, 0},

              { 0, 0, 0, 0},

              { 0, 0, 2, 0},

              { 2, 0, 0, 0}
            };

            int expectedScore = 0;
            Tile newTile = new Tile(2, 2) { Value = 2 };

            AssertMove(
                valuesBeforeMove,
                valuesAfterMove,
                expectedScore,
                (b) => b.MoveDown(),
                newTile
                );
        }

        [Fact]
        public void DownTest_2()
        {
            int[,] valuesBeforeMove = new int[,]
           {
               { 2, 0, 0, 0},

               { 2, 0, 0, 0},

               { 0, 0, 0, 0},

               { 0, 0, 0, 0}
           };


            int[,] valuesAfterMove = new int[,]
            {
              { 0, 0, 0, 0},

              { 0, 0, 0, 0},

              { 0, 0, 2, 0},

              { 4, 0, 0, 0}
            };

            int expectedScore = 4;
            Tile newTile = new Tile(2, 2) { Value = 2 };

            AssertMove(
                valuesBeforeMove,
                valuesAfterMove,
                expectedScore,
                (b) => b.MoveDown(),
                newTile
                );
        }

        [Fact]
        public void DownTest_3()
        {
            int[,] valuesBeforeMove = new int[,]
           {
               { 0, 2, 0, 0},

               { 0, 2, 0, 0},

               { 0, 2, 0, 0},

               { 0, 2, 0, 0}
           };


            int[,] valuesAfterMove = new int[,]
            {
              { 0, 0, 0, 0},

              { 0, 0, 0, 0},

              { 0, 4, 2, 0},

              { 0, 4, 0, 0}
            };

            int expectedScore = 8;
            Tile newTile = new Tile(2, 2) { Value = 2 };

            AssertMove(
                valuesBeforeMove,
                valuesAfterMove,
                expectedScore,
                (b) => b.MoveDown(),
                newTile
                );
        }

        [Fact]
        public void DownTest_4()
        {
            int[,] valuesBeforeMove = new int[,]
            {
               { 0, 2, 0, 0},

               { 0, 8, 0, 0},

               { 0, 4, 0, 0},

               { 0, 4, 0, 0}
            };


            int[,] valuesAfterMove = new int[,]
            {
              { 0, 0, 0, 0},

              { 0, 2, 0, 0},

              { 0, 8, 2, 0},

              { 0, 8, 0, 0}
            };

            int expectedScore = 8;
            Tile newTile = new Tile(2, 2) { Value = 2 };

            AssertMove(
                valuesBeforeMove,
                valuesAfterMove,
                expectedScore,
                (b) => b.MoveDown(),
                newTile
                );
        }
    }
}
