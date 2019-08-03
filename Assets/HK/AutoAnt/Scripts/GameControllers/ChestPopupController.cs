using HK.AutoAnt.CellControllers.Events;
using HK.AutoAnt.Events;
using HK.AutoAnt.UI;
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
        }
    }
}
