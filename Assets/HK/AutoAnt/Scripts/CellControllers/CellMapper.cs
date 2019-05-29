using System;
using System.Collections.Generic;
using HK.AutoAnt.CellControllers.Events;
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
        /// <see cref="Cell.Position"/>と紐づくマップ
        /// </summary>
        private readonly Dictionary<Vector2Int, Cell> map = new Dictionary<Vector2Int, Cell>();
        public IReadOnlyDictionary<Vector2Int, Cell> Map => this.map;

        /// <summary>
        /// イベントを持つ<see cref="Cell.Position"/>
        /// </summary>
        private readonly List<Vector2Int> hasEventCellIds = new List<Vector2Int>();
        public IReadOnlyList<Vector2Int> HasEventCellIds => this.hasEventCellIds;

        /// <summary>
        /// イベントが無い<see cref="Cell.Position"/>
        /// </summary>
        private readonly List<Vector2Int> notHasEventCellIds = new List<Vector2Int>();
        public IReadOnlyList<Vector2Int> NotHasEventCellIds => this.notHasEventCellIds;

        private readonly List<ICellEvent> cellEvents = new List<ICellEvent>();

        private readonly Dictionary<Vector2Int, ICellEvent> eventMap = new Dictionary<Vector2Int, ICellEvent>();
        public IReadOnlyDictionary<Vector2Int, ICellEvent> EventMap => this.eventMap;

        public void Add(Cell cell)
        {
            var position = cell.Position;
            
            Assert.IsFalse(this.cells.Contains(cell));
            Assert.IsFalse(this.map.ContainsKey(position), $"{position}の{typeof(Cell)}は既に登録されています");

            this.cells.Add(cell);
            this.map.Add(position, cell);
        }

        public void Add(ICellEvent cellEvent)
        {
            var position = cellEvent.Position;
            Assert.IsTrue(this.Map.ContainsKey(position), $"position = {position}にセルが無いのにイベントが登録されました");
            this.cellEvents.Add(cellEvent);

            for (var y = 0; y < cellEvent.Size; y++)
            {
                for (var x = 0; x < cellEvent.Size; x++)
                {
                    var p = position + new Vector2Int(x, y);
                    Assert.IsFalse(this.eventMap.ContainsKey(p), $"{p}には既にイベントが登録されています");
                    this.eventMap.Add(p, cellEvent);
                }
            }
        }

        public void Remove(Cell cell)
        {
            var position = cell.Position;

            Assert.IsTrue(this.cells.Contains(cell));
            Assert.IsTrue(this.map.ContainsKey(position), $"{position}の{typeof(Cell)}は存在しません");

            this.cells.Remove(cell);
            this.map.Remove(position);
        }

        public void Remove(ICellEvent cellEvent)
        {
            Assert.IsNotNull(cellEvent);

            var position = cellEvent.Position;
            this.cellEvents.Remove(cellEvent);

            for (var y = 0; y < cellEvent.Size; y++)
            {
                for (var x = 0; x < cellEvent.Size; x++)
                {
                    var p = position + new Vector2Int(x, y);
                    Assert.IsTrue(this.eventMap.ContainsKey(p));
                    eventMap.Remove(p);
                }
            }
        }

        public bool HasEvent(Cell cell)
        {
            return this.eventMap.ContainsKey(cell.Position);
        }

        /// <summary>
        /// イベントを持たない<see cref="Cell"/>として登録する
        /// </summary>
        public void RegisterNotHasEvent(Cell cell)
        {
            var position = cell.Position;
            Assert.IsNotNull(cell);
            Assert.IsFalse(cell.HasEvent);

            this.hasEventCellIds.Remove(position);
            this.notHasEventCellIds.Add(position);
        }

        /// <summary>
        /// イベントを持つ<see cref="Cell"/>として登録する
        /// </summary>
        public void RegisterHasEvent(Cell cell)
        {
            var position = cell.Position;
            Assert.IsNotNull(cell);
            Assert.IsTrue(cell.HasEvent);
            Assert.IsFalse(this.hasEventCellIds.Contains(position), $"{position}はすでにイベントを持っています");

            this.hasEventCellIds.Add(position);
            this.notHasEventCellIds.Remove(position);
        }

        /// <summary>
        /// <paramref name="origin"/>を原点とした範囲の座標を評価する
        /// </summary>
        /// <remarks>
        /// <paramref name="range"/>が<c>1</c>の場合は以下の範囲を評価する
        /// o = origin
        /// x = range
        /// xxx
        /// xox
        /// xxx
        /// <paramref name="range"/>が<c>2</c>の場合は以下の範囲を評価する
        /// o = origin
        /// x = range
        /// xxxxx
        /// xxxxx
        /// xxoxx
        /// xxxxx
        /// xxxxx
        /// </remarks>
        public Vector2Int[] GetRange(Vector2Int origin, int range, Func<Vector2Int, bool> selector)
        {
            Assert.IsTrue(range > 0);

            var result = new List<Vector2Int>();

            for (var y = -range; y <= range; y++)
            {
                for (var x = -range; x <= range; x++)
                {
                    var position = origin + new Vector2Int(x, y);
                    if(!selector(position))
                    {
                        continue;
                    }

                    result.Add(position);
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// <paramref name="origin"/>を原点とした範囲の座標を評価する
        /// </summary>
        /// <remarks>
        /// <paramref name="size"/>が<c>(2, 2)</c>の場合は以下の範囲を評価する
        /// o = origin
        /// x = range
        /// xx
        /// ox
        /// </remarks>
        public Vector2Int[] GetRange(Vector2Int origin, Vector2Int size, Func<Vector2Int, bool> selector)
        {
            Assert.IsTrue(size.x >= 0);
            Assert.IsTrue(size.y >= 0);

            var result = new List<Vector2Int>();

            for (var y = 0; y < size.y; y++)
            {
                for (var x = 0; x < size.x; x++)
                {
                    var position = origin + new Vector2Int(x, y);
                    if (!selector(position))
                    {
                        continue;
                    }

                    result.Add(position);
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// <paramref name="positions"/>に存在する全てのセルを返す
        /// </summary>
        public Cell[] GetCells(Vector2Int[] positions)
        {
            var result = new Cell[positions.Length];
            for (var i = 0; i < positions.Length; i++)
            {
                var position = positions[i];
                Assert.IsTrue(this.Map.ContainsKey(position));
                result[i] = this.Map[position];
            }

            return result;
        }

        /// <summary>
        /// 指定した範囲でセルが配置されていない座標を返す
        /// </summary>
        public Vector2Int[] GetEmptyPositions(Vector2Int origin, int range)
        {
            return this.GetRange(origin, range, (p) => !this.map.ContainsKey(p));
        }
    }
}
