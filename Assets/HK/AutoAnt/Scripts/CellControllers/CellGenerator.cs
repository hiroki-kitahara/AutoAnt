﻿using HK.AutoAnt.CellControllers.Events;
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

        public Cell Generate(int recordId, Vector2Int position)
        {
            var record = GameSystem.Instance.MasterData.Cell.Records.Get(recordId);
            var cell = Object.Instantiate(record.Prefab)
                .Initialize(position, record.CellType, this.cellMapper);
            cell.CachedTransform.SetParent(this.cellParent);

            return cell;
        }

        public Cell Replace(int recordId, Vector2Int position)
        {
            Assert.IsTrue(this.cellMapper.Map.ContainsKey(position), $"position = {position}にセルがないのにReplace関数が実行されました");
            
            var oldCell = this.cellMapper.Map[position];
            this.cellMapper.Remove(oldCell);
            Object.Destroy(oldCell.gameObject);

            return this.Generate(recordId, position);
        }
    }
}
