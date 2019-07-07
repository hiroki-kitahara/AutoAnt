using HK.AutoAnt.Events;
using HK.AutoAnt.UI;
using HK.Framework.EventSystems;
using HK.Framework.Text;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// セルイベントがアンロックされた事をポップアップで通知するクラス
    /// </summary>
    public sealed class NotifyUnlockCellEventController : MonoBehaviour
    {
        [SerializeField]
        private StringAsset.Finder messageFormat = null;

        [SerializeField]
        private StringAsset.Finder ok = null;

        void Awake()
        {
            Broker.Global.Receive<UnlockedCellEvent>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    var popup = PopupManager.RequestSimplePopup().Initialize(_this.messageFormat.Format(x.CellEventRecordId), _this.ok.Get);
                    popup.DecideButton.OnClickAsObservable()
                        .SubscribeWithState(popup, (_, p) =>
                        {
                            p.Close();
                        })
                        .AddTo(popup);

                    popup.Open();
                })
                .AddTo(this);
        }
    }
}
