using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers
{
    /// <summary>
    /// <see cref="Cell"/>の様々な情報をマッピングするクラス
    /// </summary>
    public sealed class CellMapper
    {
        /// <summary>
        /// 全ての<see cref="Cell"/>
        /// </summary>
        private readonly List<Cell> cells = new List<Cell>();
        public IReadOnlyList<Cell> Cells => this.cells;

        /// <summary>
        /// <see cref="Cell.Id"/>と紐づくマップ
        /// </summary>
        private readonly Dictionary<Vector2Int, Cell> map = new Dictionary<Vector2Int, Cell>();
        public IReadOnlyDictionary<Vector2Int, Cell> Map => this.map;

        /// <summary>
        /// イベントを持つ<see cref="Cell.Id"/>
        /// </summary>
        private readonly List<Vector2Int> hasEventCellIds = new List<Vector2Int>();
        public IReadOnlyList<Vector2Int> HasEventCellIds => this.hasEventCellIds;

        /// <summary>
        /// イベントが無い<see cref="Cell.Id"/>
        /// </summary>
        private readonly List<Vector2Int> notHasEventCellIds = new List<Vector2Int>();
        public IReadOnlyList<Vector2Int> NotHasEventCellIds => this.notHasEventCellIds;

        public void Add(Cell cell)
        {
            var id = cell.Id;
            
            Assert.IsFalse(this.cells.Contains(cell));
            Assert.IsFalse(this.map.ContainsKey(id), $"{id}の{typeof(Cell)}は既に登録されています");

            this.cells.Add(cell);
            this.map.Add(id, cell);
        }

        /// <summary>
        /// イベントを持たない<see cref="Cell"/>として登録する
        /// </summary>
        public void RegisterNotHasEvent(Cell cell)
        {
            var id = cell.Id;
            Assert.IsNotNull(cell);
            Assert.IsFalse(cell.HasEvent);

            this.hasEventCellIds.Remove(id);
            this.notHasEventCellIds.Add(id);
        }

        /// <summary>
        /// イベントを持つ<see cref="Cell"/>として登録する
        /// </summary>
        public void RegisterHasEvent(Cell cell)
        {
            var id = cell.Id;
            Assert.IsNotNull(cell);
            Assert.IsTrue(cell.HasEvent);
            Assert.IsFalse(this.hasEventCellIds.Contains(id), $"{id}はすでにイベントを持っています");

            this.hasEventCellIds.Add(id);
            this.notHasEventCellIds.Remove(id);
        }
    }
}
