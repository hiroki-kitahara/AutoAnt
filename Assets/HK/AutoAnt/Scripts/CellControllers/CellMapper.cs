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

        /// <summary>
        /// <see cref="Cell.Id"/>と紐づくマップ
        /// </summary>
        private readonly Dictionary<Vector2Int, Cell> map = new Dictionary<Vector2Int, Cell>();

        /// <summary>
        /// イベントを持つ<see cref="Cell.Id"/>
        /// </summary>
        private readonly List<Vector2Int> hasEventCellIds = new List<Vector2Int>();

        /// <summary>
        /// イベントが無い<see cref="Cell.Id"/>
        /// </summary>
        private readonly List<Vector2Int> notHasEventCellIds = new List<Vector2Int>();

        public void Add(Cell cell)
        {
            var id = cell.Id;
            
            Assert.IsFalse(this.cells.Contains(cell));
            Assert.IsFalse(this.map.ContainsKey(id), $"{id}の{typeof(Cell)}は既に登録されています");

            this.cells.Add(cell);
            this.map.Add(id, cell);
            if(!cell.HasEvent)
            {
                this.notHasEventCellIds.Add(id);
            }
            else
            {
                this.hasEventCellIds.Add(id);
            }
        }

        /// <summary>
        /// イベントを持たない<see cref="Cell"/>として登録する
        /// </summary>
        public void RegisterNotHasEvent(Cell cell)
        {
            var id = cell.Id;
            Assert.IsNotNull(cell);
            Assert.IsFalse(cell.HasEvent);
            Assert.AreNotEqual(this.hasEventCellIds.Contains(id), this.notHasEventCellIds.Contains(id), $"{id}はすでにイベントを持っていません");

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
            Assert.AreNotEqual(this.hasEventCellIds.Contains(id), this.notHasEventCellIds.Contains(id), $"{id}はすでにイベントを持っています");

            this.hasEventCellIds.Add(id);
            this.notHasEventCellIds.Remove(id);
        }
    }
}
