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
        public void LeftTest_1()
        {
            int[,] valuesBeforeMove = new int[,]
          {
               { 0, 0, 0, 2},

               { 0, 0, 0, 0},

               { 0, 0, 0, 0},

               { 0, 0, 0, 0}
          };


            int[,] valuesAfterMove = new int[,]
            {
              { 2, 0, 0, 0},

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
                (b) => b.MoveLeft(),
                newTile
                );
        }

        [Fact]
        public void LeftTest_2()
        {
            /*Script

            0 0 2 2         4 0 0 0

            0 0 0 0  Right  0 0 0 0
                        =>                     
            0 0 0 0         0 0 2 0

            0 0 0 0         0 0 0 0
            */

            int[,] valuesBeforeMove = new int[,]
           {
               { 0, 0, 2, 2},

               { 0, 0, 0, 0},

               { 0, 0, 0, 0},

               { 0, 0, 0, 0}
           };


            int[,] valuesAfterMove = new int[,]
            {
              { 4, 0, 0, 0},

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
                (b) => b.MoveLeft(),
                newTile
                );
        }

        [Fact]
        public void LeftTest_3()
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

              { 4, 4, 0, 0},

              { 0, 0, 0, 0}
            };

            int expectedScore = 0;
            Tile newTile = new Tile(1, 1) { Value = 2 };

            AssertMove(
                valuesBeforeMove,
                valuesAfterMove,
                expectedScore,
                (b) => b.MoveLeft(),
                newTile
                );
        }

        [Fact]
        public void LeftTest_4()
        {
            /*Script

           0 0 0 0         0 0 0 0

           0 0 0 0   Left  0 2 0 0
                       =>                     
           2 8 4 4         2 8 8 0

           0 0 0 0         0 0 0 0
           */

            int[,] valuesBeforeMove = new int[,]
            {
               { 0, 0, 0, 0},

               { 0, 0, 0, 0},

               { 2, 8, 4, 4},

               { 0, 0, 0, 0}
            };


            int[,] valuesAfterMove = new int[,]
            {
              { 0, 0, 0, 0},

              { 0, 2, 0, 0},

              { 2, 8, 8, 0},

              { 0, 0, 0, 0}
            };

            int expectedScore = 0;
            Tile newTile = new Tile(1, 1) { Value = 2 };

            AssertMove(
                valuesBeforeMove,
                valuesAfterMove,
                expectedScore,
                (b) => b.MoveLeft(),
                newTile
                );
        }
    }
}
