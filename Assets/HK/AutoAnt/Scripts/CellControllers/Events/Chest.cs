using HK.AutoAnt.Database;
using HK.AutoAnt.Extensions;
using HK.AutoAnt.GameControllers;
using HK.AutoAnt.Systems;
using HK.AutoAnt.UserControllers;
using UnityEngine;
using UnityEngine.Assertions;
using HK.AutoAnt.EffectSystems;
using HK.Framework.EventSystems;
using HK.AutoAnt.Events;
using HK.AutoAnt.UI;
using UniRx.Triggers;
using UniRx;

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
        public override void AttachFooterSelectCellEvent(FooterSelectBuildingController controller)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateFooterSelectCellEvent(FooterSelectBuildingController controller)
        {
            throw new System.NotImplementedException();
        }

        public override void OnClick(Cell owner)
        {
        }
    }
}
