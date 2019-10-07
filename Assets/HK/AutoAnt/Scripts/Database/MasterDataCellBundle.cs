﻿using System;
using System.Collections.Generic;
using HK.AutoAnt.CellControllers;
using HK.AutoAnt.CellControllers.Events;
using HK.AutoAnt.Constants;
using UnityEngine;
using UnityEngine.Assertions;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HK.AutoAnt.Database
{
    /// <summary>
    /// セルバンドルのマスターデータ
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Database/CellBundle")]
    public sealed class MasterDataCellBundle : MasterDataBase<MasterDataCellBundle.Record>
    {
        [Serializable]
        public class Record : IRecord, IRecordGroup
        {
            [SerializeField]
            private int id = 0;
            public int Id => this.id;

            [SerializeField]
            private int group = 0;
            public int Group => this.group;

            [SerializeField]
            private int cellRecordId = 0;
            public int CellRecordId => this.cellRecordId;

            [SerializeField]
            private Vector2Int position = new Vector2Int();
            public Vector2Int Position => this.position;

#if UNITY_EDITOR
            public Record(SpreadSheetData.CellBundleData data)
            {
                this.id = data.Id;
                this.group = data.Group;
                this.cellRecordId = data.Cellrecordid;
                this.position = new Vector2Int(data.X, data.Y);
            }
#endif
        }

        /// <summary>
        /// レコードIDと座標のみを持つセル
        /// </summary>
        public class Cell
        {
            public int Id { get; set; }

            public Vector2Int Position { get; set; }
        }
    }
}
