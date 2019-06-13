using System;
using System.Collections.Generic;
using HK.Framework.Text;
using UnityEngine;
using UnityEngine.Assertions;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HK.AutoAnt.Database
{
    /// <summary>
    /// アイテムのマスターデータ
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Database/Item")]
    public sealed class MasterDataItem : MasterDataBase<MasterDataItem.Record>
    {
        [Serializable]
        public class Record : IRecord, IRecordName
        {
            [SerializeField]
            private int id = 0;
            public int Id => this.id;

            [SerializeField]
            private StringAsset.Finder name = null;
            public string Name => this.name.Get;

#if UNITY_EDITOR
            public Record(SpreadSheetData.ItemData data)
            {
                this.id = data.Id;
                var stringAsset = AssetDatabase.LoadAssetAtPath<StringAsset>("Assets/HK/AutoAnt/DataSources/StringAsset/Item.asset");
                this.name = stringAsset.CreateFinderSafe(data.Name);
            }
#endif
        }
    }
}
