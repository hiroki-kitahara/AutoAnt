using UnityEngine;
using HK.AutoAnt.UI;
using System.Collections.Generic;
using HK.AutoAnt.Events;
using HK.AutoAnt.Database;
using HK.AutoAnt.Systems;
using HK.AutoAnt.Extensions;
using HK.AutoAnt.GameControllers;

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
    public sealed class Chest : CellEvent, IChest
    {
        public StackedItem[] Items { get; private set; }

        private MasterDataChestParameter.Record parameter = null;

        public override void Initialize(Vector2Int position, Systems.GameSystem gameSystem, bool isInitializingGame)
        {
            base.Initialize(position, gameSystem, isInitializingGame);
            this.parameter = GameSystem.Instance.MasterData.ChestParameter.Records.Get(this.Id);

            if(!isInitializingGame)
            {
                this.Items = new StackedItem[this.parameter.Capacity];
                for (var i = 0; i < this.parameter.Capacity; i++)
                {
                    this.Items[i] = null;
                }
            }
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

        void IChest.Add(StackedItem stackedItem)
        {
            throw new System.NotImplementedException();
        }

        void IChest.PickOut(int listId)
        {
            throw new System.NotImplementedException();
        }
    }
}
