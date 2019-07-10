using System;
using System.Collections.Generic;
using HK.Framework.Text;
using UnityEngine;
using UnityEngine.Assertions;
using HK.AutoAnt.Systems;
using HK.AutoAnt.Extensions;

#if UNITY_EDITOR
using UnityEditor;
#endif

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
            private int productId = 0;
            public int ProductId => this.productId;

            /// <summary>
            /// 生産物を生産するのに必要な時間（秒）
            /// </summary>
            [SerializeField]
            private float needProductTime = 0.0f;
            public float NeedProductTime => this.needProductTime;

            /// <summary>
            /// 人気度
            /// </summary>
            [SerializeField]
            private double popularity = 0;
            public double Popularity => this.popularity;

            /// <summary>
            /// 生産出来るアイテムのレコード
            /// </summary>
            public MasterDataItem.Record ProductRecord => GameSystem.Instance.MasterData.Item.Records.Get(this.ProductId);

#if UNITY_EDITOR
            public Record(SpreadSheetData.FacilityLevelParameterData data)
            {
                this.id = data.Id;
                this.level = data.Level;
                this.productSlot = data.Productslot;
                var stringAsset = AssetDatabase.LoadAssetAtPath<StringAsset>("Assets/HK/AutoAnt/DataSources/StringAsset/Item.asset");
                this.productId = data.Productid;
                this.needProductTime = data.Needproducttime;
                this.popularity = data.Popularity;
            }
#endif
        }
    }
}
