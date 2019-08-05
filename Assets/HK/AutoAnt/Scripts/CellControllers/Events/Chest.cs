using UnityEngine;
using HK.AutoAnt.UI;
using System.Collections.Generic;
using HK.AutoAnt.Events;
using HK.AutoAnt.Database;
using HK.AutoAnt.Systems;
using HK.AutoAnt.Extensions;
using HK.AutoAnt.GameControllers;
using System;
using UnityEngine.Assertions;
using UniRx.Triggers;
using UniRx;
using System.Linq;

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

        StackedItem[] IChest.Items => this.Items;

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

        public override void OnClick(Cell owner)
        {
            Framework.EventSystems.Broker.Global.Publish(RequestOpenChestPopup.Get(this));
        }

        bool IChest.CanAdd(StackedItem newItem)
        {
            // 空きがある場合は追加可能
            var isExistsEmpty = Array.FindIndex(this.Items, i => i == null) >= 0;
            if(isExistsEmpty)
            {
                return true;
            }

            // 空きが無く新規アイテムだった場合は追加できない
            var alreadyItem = Array.Find(this.Items, i => i.ItemId == newItem.ItemId && !i.IsFull());
            return alreadyItem != null;
        }

        StackedItem IChest.Add(StackedItem newItem)
        {
            var alreadyItemIndex = Array.FindIndex(this.Items, i => i != null && i.ItemId == newItem.ItemId && !i.IsFull());

            // 同じアイテムIDがない場合は新規で追加
            if(alreadyItemIndex == -1)
            {
                var emptyIndex = Array.FindIndex(this.Items, i => i == null);
                Assert.AreNotEqual(-1, emptyIndex);
                this.Items[emptyIndex] = newItem;
                this.Broker.Publish(UpdatedStackedItemInChest.Get(this, emptyIndex));

                return null;
            }

            var alreadyItem = this.Items[alreadyItemIndex];
            alreadyItem.Amount += newItem.Amount;

            // もしスタック数を超えた場合は超過分を空に追加する
            if (alreadyItem.IsOverflow())
            {
                newItem.Amount = alreadyItem.Amount - newItem.ItemRecord.StackNumber;
                alreadyItem.Amount = alreadyItem.ItemRecord.StackNumber;
                this.Broker.Publish(UpdatedStackedItemInChest.Get(this, alreadyItemIndex));
                var emptyIndex = Array.FindIndex(this.Items, i => i == null);

                // 空きがない場合は超過分を返す
                if (emptyIndex == -1)
                {
                    return newItem;
                }
                else
                {
                    this.Items[emptyIndex] = newItem;
                    this.Broker.Publish(UpdatedStackedItemInChest.Get(this, emptyIndex));
                    return null;
                }
            }
            else
            {
                this.Broker.Publish(UpdatedStackedItemInChest.Get(this, alreadyItemIndex));
                return null;
            }
        }

        void IChest.PickOut(int listId)
        {
            throw new System.NotImplementedException();
        }
    }
}
