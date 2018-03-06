using System;
using System.Collections.Generic;
using System.Linq;

namespace Game2048.Core
{
    /// <summary>
    /// Представляет доску для игры в 2048
    /// </summary>
    public class Board
    {
        #region Properties

        /// <summary>
        /// Текущие количество очков
        /// </summary>
        public int CurrentScore { get; private set; }

        /// <summary>
        /// Размер доски
        /// </summary>
        public int Size { get; private set; }

        #endregion

        #region Fields

        /// <summary>
        /// Плитки доски
        /// </summary>
        private Tile[] _tiles;

        /// <summary>
        /// Генератор следующих значений
        /// </summary>
        private INextGenerator _nextValueGenerator;

        #endregion

        #region Constructors

        private Board(INextGenerator nextValueGenerator, Tile[] tiles, int size, bool generateNextValue)
        {
            _nextValueGenerator = nextValueGenerator;
            _tiles = tiles;
            Size = size;
            
            if(generateNextValue)
            {
                //при создании доски сразу генерируем два новых значения
                FillNextValue();
                FillNextValue();
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Представление плиток в ввиде двумерного массива
        /// </summary>
        /// <returns></returns>
        public Tile[,] To2DArray()
        {
            Tile[,] result = new Tile[Size, Size];

            foreach (var tile in _tiles)
            {
                result[tile.X, tile.Y] = tile.Copy();
            }
            return result;
        }

        /// <summary>
        /// Получить список плиток
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Tile> GetTiles()
        {
            return _tiles.Select(t => t.Copy());
        }
        
        public bool MoveUp()
        {
            return Move(SeparateRows(false), true);
        }

        public bool MoveDown()
        {
            return Move(SeparateRows(false), false);
        }

        public bool MoveRight()
        {
            return Move(SeparateRows(true), false);
        }

        public bool MoveLeft()
        {
            return Move(SeparateRows(true), true);
        }

        /// <summary>
        ///  Возможность сделать ход
        /// </summary>
        /// <returns></returns>
        public bool NextStepAvailable()
        {
            return GetEmptyTiles().Any() || HasEqualAdjacent(SeparateRows(true)) || HasEqualAdjacent(SeparateRows(false));
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Возвращает список плиток со значением равным 0
        /// </summary>
        /// <returns></returns>
        private List<Tile> GetEmptyTiles() => _tiles.Where(c => c.Value == 0).ToList();

        /// <summary>
        /// Есть ли соседние плитки с равным значением отличным от 0 
        /// </summary>
        /// <param name="rows"></param>
        /// <returns></returns>
        private bool HasEqualAdjacent(List<Tile[]> rows)
        {
            foreach (var row in rows)
            {
                for (int i = 0; i < rows.Count - 1; i++)
                {
                    if (row[i].Value == row[i + 1].Value)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Переместить все ненулевые плитки в каждом списке в указанном направлении
        /// </summary>
        /// <param name="rows">Список строк</param>
        /// <param name="positiveStep">Направление перемещения</param>
        /// <returns></returns>
        private bool Move(List<Tile[]> rows, bool positiveStep)
        {
            bool move = false;

            foreach (var row in rows)
            {
                if (Move(row, positiveStep))
                {
                    move = true;
                }
            }
            if (move)
            {
                FillNextValue();
            }
            return move;
        }

        /// <summary>
        /// Переместить все ненулевые плитки в указанном направлении
        /// </summary>
        /// <param name="rows">Список строк</param>
        /// <param name="positiveStep">Направление перемещения</param>
        /// <returns></returns>
        private bool Move(Tile[] row, bool positiveStep)
        {
            //Просто сдвигаем все плитки к краю без слияния
            bool move = MoveAllWithoutMerge(row, positiveStep);

            //Слияние равных плиток в зависимости от указанного направления
            if (positiveStep)
            {
                for (int i = 0; i < row.Length - 1; i++)
                {
                    if (row[i].Value == row[i + 1].Value && row[i].Value != 0)
                    {
                        CurrentScore += row[i].Value * 2;

                        row[i].Value = row[i].Value * 2;
                        row[i + 1].Value = 0;

                        if (!move)
                        {
                            move = true;
                        }
                    }
                }
            }
            else
            {
                for (int i = row.Length - 1; i > 0; i--)
                {
                    if (row[i].Value == row[i - 1].Value && row[i].Value != 0)
                    {
                        CurrentScore += row[i].Value * 2;

                        row[i].Value = row[i].Value + row[i - 1].Value;
                        row[i - 1].Value = 0;

                        if (!move)
                        {
                            move = true;
                        }
                    }
                }
            }
            
            //Снова сдвигаем все плитки, так как после слияния могли оказатся пробелы
            bool moveLastAll = MoveAllWithoutMerge(row, positiveStep);

            if (!move && moveLastAll)
            {
                move = true;
            }

            return move;
        }

        /// <summary>
        /// Переместить все ненулевые плитки в указанном направлении без слияния
        /// </summary>
        /// <param name="row"></param>
        /// <param name="positiveStep"></param>
        /// <returns></returns>
        private bool MoveAllWithoutMerge(Tile[] row, bool positiveStep)
        {
            int[] nonNegativeValues = row.Where(r => r.Value != 0).Select(r => r.Value).ToArray();
            int[] newValues = new int[row.Length];

            if (positiveStep)
            {
                for (int i = 0; i < newValues.Length; i++)
                {
                    if (i >= nonNegativeValues.Length)
                    {
                        newValues[i] = 0;
                    }
                    else
                    {
                        newValues[i] = nonNegativeValues[i];
                    }
                }
            }
            else
            {
                int indexNonNegativeValues = nonNegativeValues.Length - 1;

                for (int i = newValues.Length - 1; i >= 0; i--)
                {
                    if (indexNonNegativeValues < 0)
                    {
                        newValues[i] = 0;
                    }
                    else
                    {
                        newValues[i] = nonNegativeValues[indexNonNegativeValues];
                        indexNonNegativeValues--;
                    }
                }
            }

            bool move = false;

            for (int i = 0; i < row.Length; i++)
            {
                if (row[i].Value != newValues[i])
                {
                    row[i].Value = newValues[i];
                    move = true;
                }
            }

            return move;
        }

        /// <summary>
        /// Разделить список плиток на строки
        /// </summary>
        /// <param name="hasSeparateX">Относительно какой оси будет происходить раздение</param>
        /// <returns></returns>
        private List<Tile[]> SeparateRows(bool hasSeparateX)
        {
            List<Tile[]> rows = new List<Tile[]>();

            if (hasSeparateX)
            {
                for (int i = 0; i < Size; i++)
                {
                    rows.Add(_tiles.Where(c => c.X == i).ToArray());
                }
            }
            else
            {
                for (int i = 0; i < Size; i++)
                {
                    rows.Add(_tiles.Where(c => c.Y == i).ToArray());
                }
            }
            return rows;
        }
        
        /// <summary>
        /// Заполнить следующие значение
        /// </summary>
        private void FillNextValue()
        {
            List<Tile> emptyTiles = GetEmptyTiles();

            (int x, int y) = _nextValueGenerator.NextPosition(emptyTiles);

            var nextTile = emptyTiles.First(t => t.X == x && t.Y == y);

            nextTile.Value = _nextValueGenerator.NextValue();
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Создать доску
        /// </summary>
        /// <param name="nextValueGenerator">Генератор значений</param>
        /// <param name="size">Размер</param>
        /// <remarks>
        /// При создании сразу же генерируется несколько значений
        /// </remarks>
        public static Board CreateBoard(INextGenerator nextValueGenerator, int size = 4)
        {
            if(nextValueGenerator == null)
            {
                throw new ArgumentNullException(nameof(nextValueGenerator));
            }

            if(size <= 0)
            {
                throw new ArgumentException($"Parameter \"{nameof(size)}\" must be greater than zero");
            }

            Tile[] tiles = CreateTiles(size);

            return new Board(nextValueGenerator, tiles, size, true);
        }

        /// <summary>
        /// Восстановить доску
        /// </summary>
        /// <param name="nextValueGenerator">Генератор значений</param>
        /// <param name="score">Количество очков</param>
        /// <param name="size">Размер</param>
        /// <param name="tiles">Список плиток</param>
        /// <remarks>
        /// При восстановлении доски новые значение не будут создаватся
        /// </remarks>
        /// <returns></returns>
        public static Board RestoreBoard(INextGenerator nextValueGenerator, int score, int size, Tile[] tiles)
        {
            if (nextValueGenerator == null)
            {
                throw new ArgumentNullException(nameof(nextValueGenerator));
            }

            if (size <= 0)
            {
                throw new ArgumentException($"Parameter \"{nameof(size)}\" must be greater than zero");
            }

            if (score < 0)
            {
                throw new ArgumentException($"Parameter \"{nameof(score)}\" must be greater than zero");
            }

            if(tiles == null || tiles.Length == 0)
            {
                throw new ArgumentException($"Parameter \"{nameof(tiles)}\" must be non-empty");
            }


            var board = new Board(nextValueGenerator, tiles.OrderBy(t => t.X).ThenBy(t => t.Y).ToArray(), size, false);
            board.CurrentScore = score;
            return board;
        }

        /// <summary>
        /// Создать список плиток с незаданным значением
        /// </summary>
        /// <param name="size">Размерность</param>
        /// <returns></returns>
        private static Tile[] CreateTiles(int size)
        {
            List<Tile> tiles = new List<Tile>(size * size);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    tiles.Add(new Tile(i, j));
                }
            }
            return tiles.ToArray();
        }

        #endregion
    }
}
