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
    [CreateAssetMenu(menuName = "AutoAnt/Database/UnlockCellBundle")]
    public sealed class MasterDataUnlockCellBundle : MasterDataBase<MasterDataUnlockCellBundle.Record>
    {
        [Serializable]
        public class Record : IRecord
        {
            [SerializeField]
            private int id = 0;
            public int Id => this.id;

            [SerializeField]
            private int unlockCellBundleGroup = 0;
            public int UnlockCellBundleGroup => this.unlockCellBundleGroup;

            [SerializeField]
            private int needPopulation = 0;
            public int NeedPopulation => this.needPopulation;

#if UNITY_EDITOR
            public Record(SpreadSheetData.UnlockCellBundleData data)
            {
                this.id = data.Id;
                this.unlockCellBundleGroup = data.Unlockcellbundlegroup;
                this.needPopulation = data.Needpopulation;
            }
#endif
        }
    }
}
