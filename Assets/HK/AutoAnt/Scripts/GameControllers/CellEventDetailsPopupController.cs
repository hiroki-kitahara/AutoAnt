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
    /// 
    /// </summary>
    public sealed class CellEventDetailsPopupController : MonoBehaviour
    {
        [SerializeField]
        private CellEventDetailsPopup popup;
        
        void Awake()
        {
            Broker.Global.Receive<RequestOpenCellEventDetailsPopup>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.CreatePopup(x.CellEvent);
                })
                .AddTo(this);
        }

        private void CreatePopup(CellEvent cellEvent)
        {
            var popup = PopupManager.Request(this.popup);
            popup.Initialize(cellEvent);

            Observable.Merge(
                popup.CloseButton.OnClickAsObservable(),
                InputControllers.Input.Current.DragAsObservable().AsUnitObservable(),
                Broker.Global.Receive<PopupEvents.StartOpen>().Where(x => x.Popup != popup).AsUnitObservable()
            )
                .SubscribeWithState(popup, (_, p) =>
                {
                    p.Close();
                })
                .AddTo(popup);

            popup.Open();
        }
    }
}
