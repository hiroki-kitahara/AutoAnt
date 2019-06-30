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
    /// 道路のレベルに紐づくパラメータのマスターデータ
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Database/RoadLevelParameter")]
    public sealed class MasterDataRoadLevelParameter : MasterDataBase<MasterDataRoadLevelParameter.Record>
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
            /// バフ効果を与える範囲
            /// </summary>
            [SerializeField]
            private int impactRange = 1;
            public int ImpactRange => this.impactRange;

            /// <summary>
            /// バフの倍率
            /// </summary>
            [SerializeField]
            private float effectRate = 0.0f;
            public float EffectRate => this.effectRate;

#if UNITY_EDITOR
            public Record(SpreadSheetData.RoadLevelParameterData data)
            {
                this.id = data.Id;
                this.level = data.Level;
                this.impactRange = data.Impactrange;
                this.effectRate = data.Effectrate;
            }
#endif
        }
    }
}
