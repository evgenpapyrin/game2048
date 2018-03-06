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
        public void RightTest_1()
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
              { 0, 0, 0, 2},

              { 0, 0, 0, 0},

              { 0, 0, 2, 0},

              { 0, 0, 0, 0}
            };

            int expectedScore = 0;
            Tile newTile = new Tile(2, 2) { Value = 2 };

            AssertMove(
                valuesBeforeMove,
                valuesAfterMove,
                expectedScore,
                (b) => b.MoveRight(),
                newTile
                );
        }

        [Fact]
        public void RightTest_2()
        {

            int[,] valuesBeforeMove = new int[,]
        {
               { 2, 2, 0, 0},

               { 0, 0, 0, 0},

               { 0, 0, 0, 0},

               { 0, 0, 0, 0}
        };


            int[,] valuesAfterMove = new int[,]
            {
              { 0, 0, 0, 4},

              { 0, 0, 0, 0},

              { 0, 0, 2, 0},

              { 0, 0, 0, 0}
            };

            int expectedScore = 0;
            Tile newTile = new Tile(2, 2) { Value = 2 };

            AssertMove(
                valuesBeforeMove,
                valuesAfterMove,
                expectedScore,
                (b) => b.MoveRight(),
                newTile
                );
        }

        [Fact]
        public void RightTest_3()
        {

            int[,] valuesBeforeMove = new int[,]
        {
               { 0, 0, 0, 0},

               { 0, 0, 0, 0},

               { 2, 2, 2, 2},

               { 0, 0, 0, 0}
        };


            int[,] valuesAfterMove = new int[,]
            {
              { 0, 0, 0, 0},

              { 0, 2, 0, 0},

              { 0, 0, 4, 4},

              { 0, 0, 0, 0}
            };

            int expectedScore = 0;
            Tile newTile = new Tile(1, 1) { Value = 2 };

            AssertMove(
                valuesBeforeMove,
                valuesAfterMove,
                expectedScore,
                (b) => b.MoveRight(),
                newTile
                );
        }

        [Fact]
        public void RightTest_4()
        {
            int[,] valuesBeforeMove = new int[,]
         {
               { 0, 0, 0, 0},

               { 0, 0, 0, 0},

               { 4, 4, 8, 2},

               { 0, 0, 0, 0}
         };


            int[,] valuesAfterMove = new int[,]
            {
              { 0, 0, 0, 0},

              { 0, 2, 0, 0},

              { 0, 8, 8, 2},

              { 0, 0, 0, 0}
            };

            int expectedScore = 0;
            Tile newTile = new Tile(1, 1) { Value = 2 };

            AssertMove(
                valuesBeforeMove,
                valuesAfterMove,
                expectedScore,
                (b) => b.MoveRight(),
                newTile
                );
        }
    }
}
