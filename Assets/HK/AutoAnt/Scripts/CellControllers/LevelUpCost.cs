using System;
using System.Collections.Generic;
using HK.AutoAnt.Database;
using HK.AutoAnt.Extensions;
using HK.AutoAnt.Systems;
using HK.AutoAnt.UserControllers;
using HK.Framework.Text;
using UnityEngine;
using UnityEngine.Assertions;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HK.AutoAnt.CellControllers
{
    /// <summary>
    /// セルイベントのレベルアップに必要なコスト
    /// </summary>
    [Serializable]
    public sealed class LevelUpCost
    {
        /// <summary>
        /// お金
        /// </summary>
        [SerializeField]
        private double money = 0;
        public double Money => this.money;

        /// <summary>
        /// 必要なアイテムリスト
        /// </summary>
        [SerializeField]
        private NeedItem[] needItems = new NeedItem[0];
        public NeedItem[] NeedItems => this.needItems;

#if UNITY_EDITOR
        public LevelUpCost(Database.SpreadSheetData.LevelUpCostData data)
        {
            this.money = data.Money;
            if(string.IsNullOrEmpty(data.Items))
            {
                this.needItems = new NeedItem[0];
            }
            else
            {
                var json = JsonUtility.FromJson<NeedItem.RawData.Json>(data.Items);
                this.needItems = json
                    .NeedItems
                    .Select(x => new NeedItem(x.ItemId, x.Amount)).ToArray();
            }
        }
#endif

        /// <summary>
        /// 足りているか返す
        /// </summary>
        public bool IsEnough(User user, MasterDataItem masterData)
        {
            if (!user.Wallet.IsEnoughMoney(this.money))
            {
                return false;
            }

            return Array.FindIndex(this.needItems, (n => !n.IsEnough(user.Inventory, masterData))) == -1;
        }

        /// <summary>
        /// 消費する
        /// </summary>
        public void Consume(User user, MasterDataItem masterData)
        {
            Assert.IsTrue(this.IsEnough(user, masterData));

            user.Wallet.AddMoney(-this.money);
            foreach(var n in this.needItems)
            {
                n.Consume(user.Inventory, masterData);
            }
        }

        /// <summary>
        /// 必要なアイテム
        /// </summary>
        [Serializable]
        public class NeedItem
        {
            /// <summary>
            /// アイテムの名前
            /// </summary>
            [SerializeField]
            private int itemId = 0;
            public int ItemId => this.itemId;

            /// <summary>
            /// 必要な量
            /// </summary>
            [SerializeField]
            private int amount = 0;
            public int Amount => this.amount;

#if UNITY_EDITOR
            public NeedItem(int itemId, int amount)
            {
                this.itemId = itemId;
                this.amount = amount;
            }
#endif

            /// <summary>
            /// 足りているか返す
            /// </summary>
            public bool IsEnough(Inventory inventory, MasterDataItem masterData)
            {
                var record = masterData.Records.Get(this.itemId);
                if(!inventory.Items.ContainsKey(record.Id))
                {
                    return false;
                }

                return inventory.Items[record.Id] >= this.amount;
            }

            /// <summary>
            /// 消費する
            /// </summary>
            public void Consume(Inventory inventory, MasterDataItem masterData)
            {
                Assert.IsTrue(this.IsEnough(inventory, masterData));
                var record = masterData.Records.Get(this.itemId);

                inventory.AddItem(record, -this.amount);
            }

            [Serializable]
            public class RawData
            {
                public int ItemId;
                public int Amount;

                public class Json
                {
                    public RawData[] NeedItems;
                }
            }
        }
    }
}
