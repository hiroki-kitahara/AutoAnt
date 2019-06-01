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
            foreach(var c in this.cells)
            {
                var cell = cellManager.CellGenerator.Generate(c.Id, c.Position);
                if(c.CellEvent != null)
                {
                    cellManager.EventGenerator.Generate(cell, c.CellEvent);
                }
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
