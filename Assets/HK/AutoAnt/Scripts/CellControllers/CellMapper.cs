using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers
{
    /// <summary>
    /// <see cref="Cell"/>をマッピングするクラス
    /// </summary>
    public sealed class CellMapper
    {
        /// <summary>
        /// 全ての<see cref="Cell"/>
        /// </summary>
        private readonly List<Cell> cells = new List<Cell>();

        /// <summary>
        /// <see cref="Cell.Id"/>と紐づくマップ
        /// </summary>
        private readonly Dictionary<Vector2Int, Cell> map = new Dictionary<Vector2Int, Cell>();

        public void Add(Cell cell)
        {
            this.cells.Add(cell);
            this.map.Add(cell.Id, cell);
        }
    }
}
