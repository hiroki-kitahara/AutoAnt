using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UserControllers
{
    /// <summary>
    /// 持ち物を保持する
    /// </summary>
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
        public void AddItem(int itemId, int value)
        {
            if(this.items.ContainsKey(itemId))
            {
                this.items[itemId] += value;
            }
            else
            {
                this.items.Add(itemId, value);
            }

            Debug.Log($"itemId = {itemId}, value = {this.items[itemId]}");
        }
    }
}
