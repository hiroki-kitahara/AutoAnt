using HK.AutoAnt.Database;
using HK.AutoAnt.Events;
using HK.AutoAnt.UserControllers;
using HK.Framework.EventSystems;
using HK.Framework.Text;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// 通知UIを制御するクラス
    /// </summary>
    public sealed class NotificationUIController : MonoBehaviour
    {
        [SerializeField]
        private NotificationUIElement elementPrefab = null;

        [SerializeField]
        private float delayElementDestroy = 0.0f;

        [SerializeField]
        private StringAsset.Finder acquireItemFormat = null;

        void Awake()
        {
            Broker.Global.Receive<AddedItem>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    var message = _this.acquireItemFormat.Format(x.Item.Name, x.Amount, x.Inventory.Items[x.Item.Id]);
                    _this.CreateElement(message);
                })
                .AddTo(this);

            Broker.Global.Receive<RequestNotification>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.CreateElement(x.Message);
                })
                .AddTo(this);
        }

        private void CreateElement(string message)
        {
            Instantiate(this.elementPrefab, this.transform, false).Initialize(message, this.delayElementDestroy);
        }
    }
}
