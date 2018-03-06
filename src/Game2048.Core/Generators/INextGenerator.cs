using System;
using System.Collections.Generic;
using System.Text;

namespace Game2048.Core
{
    /// <summary>
    /// Преставляет генератор новых значений для игры
    /// </summary>
    public interface INextGenerator
    {
        /// <summary>
        /// Сгенирировать следующие значение
        /// </summary>
        /// <returns></returns>
        int NextValue();

        /// <summary>
        /// Сгенировать позицию, где появится новое значение
        /// </summary>
        /// <param name="emptyTiles">Список плиток с пустым значением</param>
        /// <returns></returns>
        (int x, int y) NextPosition(List<Tile> emptyTiles);
    }
}
