using System;
using System.Collections.Generic;
using HK.AutoAnt.CellControllers.ClickEvents;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers
{
    /// <summary>
    /// フィールドを初期化するクラス
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Cell/Field Initializer")]
    public sealed class FieldInitializer : ScriptableObject
    {
        [SerializeField]
        private List<Cell> cells = new List<Cell>();

        public void Generate(CellManager cellManager)
        {
            foreach(var cell in this.cells)
            {
                cellManager.Generator.Generate(cell.Id, cell.UnitSpec.Type, cell.CellEvent);
            }
        }

        [Serializable]
        public class Cell
        {
            [SerializeField]
            private Vector2Int id;
            public Vector2Int Id => this.id;

            [SerializeField]
            private CellUnitSpec unitSpec;
            public CellUnitSpec UnitSpec => this.unitSpec;

            [SerializeField]
            private CellEvent cellEvent;
            public CellEvent CellEvent => this.cellEvent;
        }
    }
}
