using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HK.AutoAnt.CellControllers.Events;
using HK.AutoAnt.Database;
using HK.AutoAnt.Events;
using HK.AutoAnt.Systems;
using HK.AutoAnt.UI;
using HK.AutoAnt.UI.Elements;
using HK.AutoAnt.UserControllers;
using HK.Framework.EventSystems;
using UniRx;
using UniRx.Triggers;
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
                Broker.Global.Publish(RequestNotification.Get($"最大レベルです！", NotificationUIElement.MessageType.Error));
                return false;
            }

            if (!levelUpCostRecord.Cost.IsEnough(gameSystem.User, gameSystem.MasterData.Item))
            {
                // FIXME: 不要になったら削除する
                Broker.Global.Publish(RequestNotification.Get($"素材が足りません！", NotificationUIElement.MessageType.Error));
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
            gameSystem.User.History.GenerateCellEvent.Add(self.Id, self.Level - 1);

            // FIXME: 不要になったら削除する
            Broker.Global.Publish(RequestNotification.Get($"レベルアップ！ {self.Level - 1} -> {self.Level}", NotificationUIElement.MessageType.Information));
        }

        public static void AttachDetailsPopup(this ILevelUpEvent self, CellEventDetailsPopup popup, GameSystem gameSystem)
        {
            var levelUpCostRecord = gameSystem.MasterData.LevelUpCost.Records.Get(self.Id, self.Level);
            if (levelUpCostRecord == null)
            {
                return;
            }

            var properties = new List<Property>();

            // お金を表示
            properties.Add(
                popup.AddLevelUpCost(property =>
                {
                    var stringBuilder = new StringBuilder();
                    var color = (gameSystem.User.Wallet.Money >= levelUpCostRecord.Cost.Money) ? popup.EnoughLevelUpCostColor : popup.NotEnoughLevelUpCostColor;
                    property.Prefix.text = popup.Money.Get;
                    property.Value.text = stringBuilder.AppendColorCode(color, levelUpCostRecord.Cost.Money.ToReadableString("###")).ToString();
                })
            );

            // アイテムを表示
            foreach(var n in levelUpCostRecord.Cost.NeedItems)
            {
                properties.Add(
                    popup.AddLevelUpCost(property =>
                    {
                        var inventoryItem = gameSystem.User.Inventory.Items;
                        var itemRecord = gameSystem.MasterData.Item.Records.Get(n.ItemId);
                        var possessionItemAmount = inventoryItem.ContainsKey(itemRecord.Id) ? inventoryItem[itemRecord.Id] : 0;
                        var stringBuilder = new StringBuilder();
                        var color = (possessionItemAmount >= n.Amount) ? popup.EnoughLevelUpCostColor : popup.NotEnoughLevelUpCostColor;
                        property.Prefix.text = itemRecord.Name;
                        property.Value.text = stringBuilder.AppendColorCode(color, popup.NeedItemValue.Format(possessionItemAmount, n.Amount)).ToString();
                    })
                );
            }

            // ポップアップを開いているときもお金やアイテムの所持数が更新されるのを考慮してプロパティも更新する
            popup.UpdateAsObservable()
                .SubscribeWithState(properties, (_, _properties) =>
                {
                    foreach(var p in _properties)
                    {
                        p.UpdateProperty();
                    }
                });
        }

        public static void AttachFooterSelectCellEvent(this ILevelUpEvent self, FooterSelectBuildingController controller, GameSystem gameSystem)
        {
            var levelUpCostRecord = gameSystem.MasterData.LevelUpCost.Records.Get(self.Id, 0);
            controller.CellEventName.text = self.EventName;
            controller.Size.text = controller.SizeFormat.Format(self.Size);
            var possessionMoney = gameSystem.User.Wallet.Money;
            var stringBuilder = new StringBuilder();
            controller.SetMoney(stringBuilder, gameSystem.User.Wallet.Money, levelUpCostRecord.Cost.Money);
            controller.CreateGimmick(self.GimmickPrefab);

            var properties = new List<Property>();

            // アイテムを表示
            foreach (var n in levelUpCostRecord.Cost.NeedItems)
            {
                properties.Add(
                    controller.AddProperty(property =>
                    {
                        var inventoryItem = gameSystem.User.Inventory.Items;
                        var itemRecord = gameSystem.MasterData.Item.Records.Get(n.ItemId);
                        var possessionItemAmount = inventoryItem.ContainsKey(itemRecord.Id) ? inventoryItem[itemRecord.Id] : 0;
                        stringBuilder.Clear();
                        var color = controller.GetConditionColor(possessionItemAmount >= n.Amount);
                        property.Prefix.text = itemRecord.Name;
                        property.Value.text = stringBuilder.AppendColorCode(color, controller.NeedItemFormat.Format(possessionItemAmount, n.Amount)).ToString();
                    })
                );
            }

            // お金を毎フレーム更新する
            var t = new Tuple<StringBuilder, GameSystem, FooterSelectBuildingController, MasterDataLevelUpCost.Record>(
                stringBuilder,
                gameSystem,
                controller,
                levelUpCostRecord
                );
            controller.UpdateAsObservable()
                .TakeUntilDisable(controller)
                .SubscribeWithState(t, (_, _t) =>
                {
                    var _stringBuilder = _t.Item1;
                    var _gameSystem = _t.Item2;
                    var _controller = _t.Item3;
                    var _levelUpCostRecord = _t.Item4;
                    _controller.SetMoney(_stringBuilder, _gameSystem.User.Wallet.Money, _levelUpCostRecord.Cost.Money);
                });

            // フッターを開いているときもお金やアイテムの所持数が更新されるのを考慮してプロパティも更新する
            controller.UpdateAsObservable()
                .TakeUntilDisable(controller)
                .SubscribeWithState3(properties, stringBuilder, gameSystem, (_, _properties, _stringBuilder, _gameSystem) =>
                {
                    foreach (var p in _properties)
                    {
                        p.UpdateProperty();
                    }
                });
        }
    }
}
