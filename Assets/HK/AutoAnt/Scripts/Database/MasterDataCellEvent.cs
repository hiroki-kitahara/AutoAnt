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
    /// セルイベントのマスターデータ
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Database/CellEvent")]
    public sealed class MasterDataCellEvent : MasterDataBase<MasterDataCellEvent.Record>
    {
        [Serializable]
        public class Record : IRecord
        {
            [SerializeField]
            private int id = 0;
            public int Id => this.id;

            [SerializeField]
            private CellEvent eventData = null;
            public CellEvent EventData => this.eventData;
        }
    }
}
