using System.Collections.Generic;
using System.Linq;
using HK.AutoAnt.CellControllers.Events;
using HK.AutoAnt.Database;
using HK.AutoAnt.Events;
using HK.AutoAnt.Systems;
using HK.AutoAnt.UI;
using HK.AutoAnt.UserControllers;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Extensions
{
    /// <summary>
    /// <see cref="CellControllers.Events.ILevelUpEvent"/>に関する拡張関数
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// レベルアップ可能か返す
        /// </summary>
        public static bool CanLevelUp(this ILevelUpEvent self, GameSystem gameSystem)
        {
            var levelUpCostRecord = gameSystem.MasterData.LevelUpCost.Records.Get(self.Id, self.Level);
            if (levelUpCostRecord == null)
            {
                // FIXME: 不要になったら削除する
                Broker.Global.Publish(RequestNotification.Get($"最大レベルです！"));
                return false;
            }

            if (!levelUpCostRecord.Cost.IsEnough(gameSystem.User, gameSystem.MasterData.Item))
            {
                // FIXME: 不要になったら削除する
                Broker.Global.Publish(RequestNotification.Get($"素材が足りません！"));
                return false;
            }

            return true;
        }

        public static void LevelUp(this ILevelUpEvent self, GameSystem gameSystem)
        {
            var levelUpCostRecord = gameSystem.MasterData.LevelUpCost.Records.Get(self.Id, self.Level);

            levelUpCostRecord.Cost.Consume(gameSystem.User, gameSystem.MasterData.Item);
            self.Level++;
            var record = gameSystem.MasterData.CellEvent.Records.Get(self.Id);

            // FIXME: 不要になったら削除する
            Broker.Global.Publish(RequestNotification.Get($"レベルアップ！ {self.Level - 1} -> {self.Level}"));
        }

        public static void AttachDetailsPopup(this ILevelUpEvent self, CellEventDetailsPopup popup, GameSystem gameSystem)
        {
            var levelUpCostRecord = gameSystem.MasterData.LevelUpCost.Records.Get(self.Id, self.Level);
            popup.AddLevelUpCost(property =>
            {
                property.Prefix.text = popup.Money.Get;
                property.Value.text = levelUpCostRecord.Cost.Money.ToReadableString("###");
            });
            foreach(var n in levelUpCostRecord.Cost.NeedItems)
            {
                popup.AddLevelUpCost(property =>
                {
                    var inventoryItem = gameSystem.User.Inventory.Items;
                    var itemRecord = gameSystem.MasterData.Item.Records.Get(n.ItemName);
                    var possessionItemAmount = inventoryItem.ContainsKey(itemRecord.Id) ? inventoryItem[itemRecord.Id] : 0;
                    property.Prefix.text = n.ItemName;
                    property.Value.text = popup.NeedItemValue.Format(possessionItemAmount, n.Amount);
                });
            }
        }
    }
}
