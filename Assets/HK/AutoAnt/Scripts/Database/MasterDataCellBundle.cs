using System;
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
        public class Record : IRecord
        {
            [SerializeField]
            private int id = 0;
            public int Id => this.id;

            [SerializeField]
            private int group = 0;
            public int Group => this.group;

            [SerializeField]
            private Rect rect = Rect.zero;
            public Rect Rect => this.rect;

#if UNITY_EDITOR
            public Record(SpreadSheetData.CellBundleData data)
            {
                this.id = data.Id;
                this.group = data.Group;
                this.rect = new Rect(data.X, data.Y, data.Width, data.Height);
            }
#endif
        }
    }
}
