using System;
using System.Collections.Generic;
using HK.AutoAnt.CellControllers;
using HK.AutoAnt.CellControllers.Events;
using HK.AutoAnt.Constants;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Database
{
    /// <summary>
    /// レベルアップコストのマスターデータ
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Database/LevelUpCost")]
    public sealed class MasterDataLevelUpCost : MasterDataBase<MasterDataLevelUpCost.Record>
    {
        [Serializable]
        public class Record : IRecord
        {
            /// <summary>
            /// <see cref="MasterDataCellEvent.Record.Id"/>に紐づくID
            /// </summary>
            [SerializeField]
            private int id = 0;
            public int Id => this.id;

            /// <summary>
            /// コスト
            /// </summary>
            [SerializeField]
            private LevelUpCost cost;
            public LevelUpCost Cost => this.cost;
        }
    }
}
