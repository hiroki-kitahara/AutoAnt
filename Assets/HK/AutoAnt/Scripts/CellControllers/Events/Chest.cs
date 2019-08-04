using UnityEngine;
using HK.AutoAnt.UI;
using System.Collections.Generic;
using HK.AutoAnt.Events;

namespace HK.AutoAnt.CellControllers.Events
{
    /// <summary>
    /// 貯蔵のセルイベント
    /// </summary>
    /// <remarks>
    /// - やっていること
    ///     - アイテムを貯蔵出来る
    /// </remarks>
    [CreateAssetMenu(menuName = "AutoAnt/Cell/Event/Chest")]
    public sealed class Chest : CellEvent
    {
        public List<int> Items { get; private set; } = new List<int>();
        
        public override void AttachFooterSelectCellEvent(FooterSelectBuildingController controller)
        {
        }

        public override void UpdateFooterSelectCellEvent(FooterSelectBuildingController controller)
        {
        }

        public override void OnClick(Cell owner)
        {
            Framework.EventSystems.Broker.Global.Publish(RequestOpenChestPopup.Get(this));
        }
    }
}
