using System;
using System.Collections.Generic;
using HK.AutoAnt.Database;
using HK.AutoAnt.Extensions;
using HK.AutoAnt.Systems;
using HK.AutoAnt.UserControllers;
using HK.Framework.Text;
using UnityEngine;
using UnityEngine.Assertions;

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
        private int money;

        /// <summary>
        /// 必要なアイテムリスト
        /// </summary>
        [SerializeField]
        private List<NeedItem> needItems = new List<NeedItem>();

        /// <summary>
        /// 足りているか返す
        /// </summary>
        public bool IsEnough(User user, MasterDataItem masterData)
        {
            if (!user.Wallet.IsEnoughMoney(this.money))
            {
                return false;
            }

            return this.needItems.FindIndex(n => !n.IsEnough(user.Inventory, masterData)) == -1;
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
            private StringAsset.Finder itemName;

            /// <summary>
            /// 必要な量
            /// </summary>
            [SerializeField]
            private int amount;

            /// <summary>
            /// 足りているか返す
            /// </summary>
            public bool IsEnough(Inventory inventory, MasterDataItem masterData)
            {
                var record = masterData.Records.Get(this.itemName.Get);
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
                var record = masterData.Records.Get(this.itemName.Get);

                inventory.AddItem(record, -this.amount);
            }
        }
    }
}
