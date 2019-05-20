﻿using System.Collections.Generic;
using HK.AutoAnt.CellControllers.ClickEvents;
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
        private CellType cellType;
        public CellType Type => this.cellType;

        [SerializeField]
        private Cell prefab;
        public Cell Prefab => this.prefab;

        [SerializeField]
        private List<CellClickEvent> clickEvents = new List<CellClickEvent>();
        public List<CellClickEvent> ClickEvents => this.clickEvents;
    }
}