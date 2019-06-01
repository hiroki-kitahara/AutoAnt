using System;
using System.Collections.Generic;
using HK.AutoAnt.Database;
using HK.AutoAnt.Events;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UserControllers
{
    /// <summary>
    /// 持ち物を保持する
    /// </summary>
    [Serializable]
    public sealed class Inventory
    {
        /// <summary>
        /// アイテムリスト
        /// </summary>
        /// <remarks>
        /// key = itemId
        /// value = 個数
        /// </remarks>
        public IReadOnlyDictionary<int, int> Items => this.items;
        private readonly Dictionary<int, int> items = new Dictionary<int, int>();

        /// <summary>
        /// アイテムを追加する
        /// </summary>
        public void AddItem(MasterDataItem.Record item, int amount)
        {
            var itemId = item.Id;
            if(this.items.ContainsKey(itemId))
            {
                this.items[itemId] += amount;
            }
            else
            {
                this.items.Add(itemId, amount);
            }

            Broker.Global.Publish(AddedItem.Get(this, item, amount));
        }
    }
}
