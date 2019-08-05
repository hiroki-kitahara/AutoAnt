using HK.AutoAnt.CellControllers.Events;
using HK.AutoAnt.Events;
using HK.AutoAnt.UI;
using HK.AutoAnt.UI.Elements;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// 貯蔵ポップアップを制御するクラス
    /// </summary>
    public sealed class ChestPopupController : MonoBehaviour
    {
        [SerializeField]
        private ChestPopup popupPrefab = null;

        void Awake()
        {
            Broker.Global.Receive<RequestOpenChestPopup>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.OpenPopup(x.Chest);
                })
                .AddTo(this);
        }

        private void OpenPopup(Chest chest)
        {
            var popup = PopupManager.Request(this.popupPrefab);
            popup.Title.text = chest.EventName;
            popup.GridList.SetData(chest.Items, this.ApplyGridListElement);
            popup.CloseButton.OnClickAsObservable()
                .SubscribeWithState(popup, (_, _popup) =>
                {
                    _popup.Close();
                })
                .AddTo(popup);

            chest.Broker.Receive<UpdatedStackedItemInChest>()
                .SubscribeWithState2(this, popup, (x, _this ,p) =>
                {
                    p.GridList.UpdateData(x.Chest.Items, x.ItemsIndex, _this.ApplyGridListElement);
                })
                .AddTo(popup);

            popup.Open();
        }

        private void ApplyGridListElement(int itemsIndex, StackedItem stackedItem, GridListElement element)
        {
            element.Clear();

            if (stackedItem == null)
            {
                return;
            }

            element.SetValue(stackedItem.ItemRecord.IconToSprite);
            element.Amount.text = stackedItem.Amount.ToString();
        }
    }
}
