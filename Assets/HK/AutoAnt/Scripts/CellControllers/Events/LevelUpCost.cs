using System;
using System.Collections.Generic;
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

        public bool IsEnough(GameSystem gameSystem)
        {
            var user = gameSystem.User;
            if (!user.Wallet.IsEnoughMoney(this.money))
            {
                return false;
            }

            return this.needItems.FindIndex(n => !n.IsEnough(gameSystem)) == -1;
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

            public bool IsEnough(GameSystem gameSystem)
            {
                var masterData = gameSystem.MasterData.Item.Records.Get(this.itemName.Get);
                var inventory = gameSystem.User.Inventory;
                if(!inventory.Items.ContainsKey(masterData.Id))
                {
                    return false;
                }

                return inventory.Items[masterData.Id] >= this.amount;
            }
        }
    }
}
