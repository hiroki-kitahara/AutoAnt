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
                if(c.CellEventRecordId != 0)
                {
                    cellManager.EventGenerator.Generate(cell, c.CellEventRecordId, true);
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
            private int cellEventRecordId = 0;
            public int CellEventRecordId => this.cellEventRecordId;
        }
    }
}
