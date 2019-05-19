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

        private readonly CellPrefabs cellPrefabs;

        public CellGenerator(CellSpec cellSpec, CellPrefabs cellPrefabs)
        {
            this.cellSpec = cellSpec;
            this.cellPrefabs = cellPrefabs;
        }

        public Cell Generate(Vector2Int id, CellType cellType, Transform parent, ICellClickEvent clickEvent)
        {
            var cell = Object.Instantiate(this.cellPrefabs.Get(cellType)).Initialize(id, cellType, this.cellSpec, clickEvent);
            cell.CachedTransform.SetParent(parent);

            return cell;
        }
    }
}
