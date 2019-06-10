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
    /// レベルアップコストのマスターデータ
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Database/LevelUpCost")]
    public sealed class MasterDataLevelUpCost : MasterDataBase<MasterDataLevelUpCost.Record>
    {
        [Serializable]
        public class Record : IRecord, IRecordLevel
        {
            /// <summary>
            /// <see cref="MasterDataCellEvent.Record.Id"/>に紐づくID
            /// </summary>
            [SerializeField]
            private int id = 0;
            public int Id => this.id;

            /// <summary>
            /// <see cref="MasterDataCellEvent.Record.Id"/>の該当するレベル
            /// </summary>
            /// <remarks>
            /// <c>0</c>の場合は生産する際に必要なコストとなります
            /// </remarks>
            [SerializeField]
            private int level = 0;
            public int Level => this.level;

            /// <summary>
            /// コスト
            /// </summary>
            [SerializeField]
            private LevelUpCost cost = null;
            public LevelUpCost Cost => this.cost;

#if UNITY_EDITOR
            public Record(SpreadSheetData.LevelUpCostData data)
            {
                this.id = data.Id;
                this.level = data.Level;
                this.cost = new LevelUpCost(data);
            }
#endif
        }
    }
}
