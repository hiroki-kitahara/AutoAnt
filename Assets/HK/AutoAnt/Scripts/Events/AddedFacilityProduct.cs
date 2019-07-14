using HK.AutoAnt.CellControllers.Events;
using HK.AutoAnt.Database;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Events
{
    /// <summary>
    /// 商業施設の生産物が追加された際のイベント
    /// </summary>
    public sealed class AddedFacilityProduct : Message<AddedFacilityProduct, Facility, MasterDataItem.Record>
    {
        public Facility Facility => this.param1;

        public MasterDataItem.Record Product => this.param2;
    }
}
