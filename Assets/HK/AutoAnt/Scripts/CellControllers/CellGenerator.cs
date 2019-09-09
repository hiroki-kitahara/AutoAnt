using System.Collections.Generic;
using HK.AutoAnt.CellControllers.Events;
using HK.AutoAnt.Constants;
using HK.AutoAnt.Extensions;
using HK.AutoAnt.Systems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers
{
    /// <summary>
    /// <see cref="Cell"/>を生成するクラス
    /// </summary>
    public sealed class CellGenerator
    {
        private readonly CellMapper cellMapper;

        private Transform cellParent;

        public CellGenerator(CellMapper cellMapper, Transform cellParent)
        {
            this.cellMapper = cellMapper;
            this.cellParent = cellParent;
        }

        public Cell Generate(int recordId, Vector2Int position, int group)
        {
            var record = GameSystem.Instance.MasterData.Cell.Records.Get(recordId);
            var cell = Object.Instantiate(record.Prefab)
                .Initialize(recordId, position, group, record.CellType, this.cellMapper);
            cell.CachedTransform.SetParent(this.cellParent);

            return cell;
        }

        public List<Cell> GenerateFromCellBundle(int group)
        {
            var result = new List<Cell>();
            var targets = GameSystem.Instance.MasterData.CellBundle.Get(group);
            foreach(var t in targets)
            {
                result.Add(this.Generate(t.Id, t.Position, group));
            }

            return result;
        }

        public Cell Replace(int recordId, Vector2Int position)
        {
            Assert.IsTrue(this.cellMapper.Cell.Map.ContainsKey(position), $"position = {position}にセルがないのにReplace関数が実行されました");
            
            var oldCell = this.cellMapper.Cell.Map[position];
            this.cellMapper.Remove(oldCell);
            Object.Destroy(oldCell.gameObject);

            return this.Generate(recordId, position, oldCell.Group);
        }
    }
}
