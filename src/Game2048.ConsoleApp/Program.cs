using ConsoleTables;
using Game2048.Core;
using Game2048.Core.Generators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Game2048.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = Board.CreateBoard(new NextGenerator());

            DrawBoard(board);
            
            while (true)
            {
                var readKey = Console.ReadKey();

                switch (readKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        board.MoveUp();
                        break;
                    case ConsoleKey.DownArrow:
                        board.MoveDown();
                        break;
                    case ConsoleKey.LeftArrow:
                        board.MoveLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        board.MoveRight();
                        break;
                }

                if(readKey.Key == ConsoleKey.X)
                {
                    break;
                }

                DrawBoard(board);
            }
        }

        public static void DrawBoard(Board board)
        {
            Tile[,] tiles = board.To2DArray();
            
            Console.Clear();

            Console.WriteLine($"Score: \"{board.CurrentScore}\"");
            Console.WriteLine();

            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    Console.Write($"{tiles[i, j].Value, 4}");
                }

                Console.WriteLine();
                Console.WriteLine();
            }

            Console.WriteLine("Press \"C\" to shut down.");
        }
    }
}
