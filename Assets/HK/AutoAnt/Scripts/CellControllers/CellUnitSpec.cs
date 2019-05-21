using System.Collections.Generic;
using HK.AutoAnt.CellControllers.Events;
using HK.AutoAnt.Constants;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers
{
    /// <summary>
    /// <see cref="Cell"/>の単体の基本情報
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Cell/Unit Spec")]
    public sealed class CellUnitSpec : ScriptableObject
    {
        [SerializeField]
        private CellType cellType = CellType.Grassland;
        public CellType Type => this.cellType;

        [SerializeField]
        private Cell prefab = null;
        public Cell Prefab => this.prefab;

        [SerializeField]
        private List<CellEvent> clickEvents = new List<CellEvent>();
        public List<CellEvent> ClickEvents => this.clickEvents;
    }
}
