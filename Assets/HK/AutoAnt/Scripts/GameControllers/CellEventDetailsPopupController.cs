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
                    popup = PopupManager.Request(_this.popup);
                    popup.Initialize(x.CellEvent);
                    popup.Open();
                })
                .AddTo(this);
        }
    }
}
