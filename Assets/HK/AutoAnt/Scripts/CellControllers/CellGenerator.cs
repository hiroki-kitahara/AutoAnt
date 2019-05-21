using HK.AutoAnt.CellControllers.ClickEvents;
using HK.AutoAnt.Constants;
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

        public Cell Generate(Vector2Int id, CellType cellType, ICellEvent clickEvent)
        {
            var cell = Object.Instantiate(this.cellSpec.GetPrefab(cellType))
                .Initialize(id, cellType, this.cellSpec, clickEvent, this.cellMapper);
            cell.CachedTransform.SetParent(this.cellParent);

            return cell;
        }
    }
}
