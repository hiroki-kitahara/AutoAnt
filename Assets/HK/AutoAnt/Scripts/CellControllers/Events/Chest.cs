using UnityEngine;
using HK.AutoAnt.UI;
using System.Collections.Generic;
using HK.AutoAnt.Events;
using HK.AutoAnt.Database;
using HK.AutoAnt.Systems;
using HK.AutoAnt.Extensions;

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

        private MasterDataChestParameter.Record parameter = null;

        public override void Initialize(Vector2Int position, Systems.GameSystem gameSystem, bool isInitializingGame)
        {
            base.Initialize(position, gameSystem, isInitializingGame);
            this.parameter = GameSystem.Instance.MasterData.ChestParameter.Records.Get(this.Id);
        }

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
