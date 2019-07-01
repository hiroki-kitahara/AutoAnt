using HK.AutoAnt.CellControllers.Events;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Events
{
    /// <summary>
    /// 商業施設の生産物を獲得した際のイベント
    /// </summary>
    public sealed class AcquiredFacilityProduct : Message<AcquiredFacilityProduct, Facility>
    {
        public Facility Facility => this.param1;
    }
}
