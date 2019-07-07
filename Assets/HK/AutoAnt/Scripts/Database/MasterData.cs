using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Database
{
    /// <summary>
    /// 全てのマスターデータを保持するクラス
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Database/MasterData")]
    public sealed class MasterData : ScriptableObject
    {
        [SerializeField]
        private MasterDataItem item = null;
        public MasterDataItem Item => this.item;

        [SerializeField]
        private MasterDataCell cell = null;
        public MasterDataCell Cell => this.cell;

        [SerializeField]
        private MasterDataCellEvent cellEvent = null;
        public MasterDataCellEvent CellEvent => this.cellEvent;

        [SerializeField]
        private MasterDataLevelUpCost levelUpCost = null;
        public MasterDataLevelUpCost LevelUpCost => this.levelUpCost;

        [SerializeField]
        private MasterDataHousingLevelParameter housingLevelParameter = null;
        public MasterDataHousingLevelParameter HousingLevelParameter => this.housingLevelParameter;

        [SerializeField]
        private MasterDataFacilityLevelParameter facilityLevelParameter = null;
        public MasterDataFacilityLevelParameter FacilityLevelParameter => this.facilityLevelParameter;

        [SerializeField]
        private MasterDataRoadLevelParameter roadLevelParameter = null;
        public MasterDataRoadLevelParameter RoadLevelParameter => this.roadLevelParameter;

        [SerializeField]
        private MasterDataUnlockCellEvent unlockCellEvent = null;
        public MasterDataUnlockCellEvent UnlockCellEvent => this.unlockCellEvent;
    }
}
