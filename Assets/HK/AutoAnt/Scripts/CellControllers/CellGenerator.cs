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

        public CellGenerator(CellSpec cellSpec)
        {
            this.cellSpec = cellSpec;
        }

        public Cell Generate(Vector2Int id, CellType cellType, Transform parent, ICellClickEvent clickEvent, CellMapper cellMapper)
        {
            var cell = Object.Instantiate(this.cellSpec.GetPrefab(cellType))
                .Initialize(id, cellType, this.cellSpec, clickEvent, cellMapper);
            cell.CachedTransform.SetParent(parent);

            return cell;
        }
    }
}
