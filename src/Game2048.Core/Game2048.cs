using Game2048.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game2048.Core
{
    /// <summary>
    /// Представляет игру 2048
    /// </summary>
    public class Game
    {
        #region Constants

        public const int WIN_SCORE = 2048;

        #endregion

        #region Constructors

        public Game(int id, int userID, Board board)
        {
            ID = id;
            Board = board;
            UserID = userID;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Уникальные идентификатор игры
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Уникальный идентификатор игрока
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Количество очков
        /// </summary>
        public int Score
        {
            get { return Board.CurrentScore; }
        }

        /// <summary>
        /// Определяет доступен ли следующий ход
        /// </summary>
        public bool NextStepAvailable
        {
            get { return Board.NextStepAvailable(); }
        }

        /// <summary>
        /// Доска для игры
        /// </summary>
        public Board Board { get; private set; }

        /// <summary>
        /// Статус игры
        /// </summary>
        public StateGame StateGame
        {
            get
            {
                if (Score == WIN_SCORE)
                {
                    return StateGame.Win;
                }
                else if (!Board.NextStepAvailable())
                {
                    return StateGame.Lose;
                }
                else
                {
                    return StateGame.Progress;
                }
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Сделать ход в указанном направлении
        /// </summary>
        /// <param name="move"></param>
        public void Move(MoveType move)
        {
            if(Board.NextStepAvailable())
            {
                switch (move)
                {
                    case MoveType.Up:
                        Board.MoveUp();
                        break;
                    case MoveType.Down:
                        Board.MoveDown();
                        break;
                    case MoveType.Left:
                        Board.MoveLeft();
                        break;
                    case MoveType.Right:
                        Board.MoveRight();
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        #endregion
    }

    #region Enums
    
    /// <summary>
    /// Статус игры
    /// </summary>
    public enum StateGame
    {
        Progress,
        Win,
        Lose
    }

    /// <summary>
    /// Направление хода
    /// </summary>
    public enum MoveType
    {
        Up,
        Down,
        Left,
        Right
    }

    #endregion
}
