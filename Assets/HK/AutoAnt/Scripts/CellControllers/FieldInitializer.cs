using System;
using System.Collections.Generic;
using HK.AutoAnt.CellControllers.Events;
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
                cellManager.Generator.Generate(cell.Id, cell.Position, cell.CellEvent);
            }
        }

        [Serializable]
        public class Cell
        {
            [SerializeField]
            private int id = 0;
            public int Id => this.id;

            [SerializeField]
            private Vector2Int position = Vector2Int.zero;
            public Vector2Int Position => this.position;

            [SerializeField]
            private CellEvent cellEvent = null;
            public CellEvent CellEvent => this.cellEvent;
        }
    }
}
