using System;
using System.Collections.Generic;
using HK.Framework.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Database
{
    /// <summary>
    /// 施設のレベルに紐づくパラメータのマスターデータ
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Database/FacilityLevelParameter")]
    public sealed class MasterDataFacilityLevelParameter : MasterDataBase<MasterDataFacilityLevelParameter.Record>
    {
        [Serializable]
        public class Record : IRecord, IRecordLevel
        {
            [SerializeField]
            private int id = 0;
            public int Id => this.id;

            [SerializeField]
            private int level = 0;
            public int Level => this.level;

            /// <summary>
            /// 生産物を格納できる数
            /// </summary>
            [SerializeField]
            private int productSlot = 1;
            public int ProductSlot => this.productSlot;

            /// <summary>
            /// 生産できるアイテム名
            /// </summary>
            [SerializeField]
            private StringAsset.Finder productName = null;
            public string ProductName => this.productName.Get;

            /// <summary>
            /// 人気度
            /// </summary>
            [SerializeField]
            private int popularity = 0;
            public int Popularity => this.popularity;
        }
    }
}
