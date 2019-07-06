using HK.AutoAnt.Events;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// ポップアップを管理するクラス
    /// </summary>
    public sealed class PopupManager : MonoBehaviour
    {
        [SerializeField]
        private SimplePopup simplePopup = null;

        private static PopupManager instance;

        void Awake()
        {
            Assert.IsNull(instance);
            instance = this;
        }

        void OnDestroy()
        {
            Assert.IsNotNull(instance);
            instance = null;
        }

        public static T Request<T>(T prefab) where T : Popup
        {
            var popup = Instantiate(prefab, instance.transform, false);
            Broker.Global.Receive<PopupEvents.CompleteClose>()
                .Where(x => x.Popup == popup)
                .SubscribeWithState(popup, (_, _popup) =>
                {
                    Destroy(_popup.gameObject);
                })
                .AddTo(popup);

            return popup;
        }

        public static SimplePopup RequestSimplePopup()
        {
            return Request(instance.simplePopup);
        }
    }
}
