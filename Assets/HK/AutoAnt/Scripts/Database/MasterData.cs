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
    }
}
