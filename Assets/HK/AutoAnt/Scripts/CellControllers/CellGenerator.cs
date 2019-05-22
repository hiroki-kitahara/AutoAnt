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
        private readonly CellSpec cellSpec;

        private readonly CellMapper cellMapper;

        private Transform cellParent;

        public CellGenerator(CellSpec cellSpec, CellMapper cellMapper, Transform cellParent)
        {
            this.cellSpec = cellSpec;
            this.cellMapper = cellMapper;
            this.cellParent = cellParent;
        }

        public Cell Generate(int masterDataId, Vector2Int position, ICellEvent clickEvent)
        {
            var record = GameSystem.Instance.MasterData.Cell.Records.Get(masterDataId);
            var cell = Object.Instantiate(record.Prefab)
                .Initialize(position, record.CellType, this.cellSpec, clickEvent, this.cellMapper);
            cell.CachedTransform.SetParent(this.cellParent);

            return cell;
        }
    }
}
