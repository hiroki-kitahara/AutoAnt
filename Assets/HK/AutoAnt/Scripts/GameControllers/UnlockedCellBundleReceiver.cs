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
    /// <see cref="CellBundle"/>のアンロック通知を受信するクラス
    /// </summary>
    public sealed class UnlockedCellBundleReceiver : MonoBehaviour
    {
        [SerializeField]
        StringAsset.Finder message = null;

        [SerializeField]
        StringAsset.Finder okMessage = null;

        void Awake()
        {
            Broker.Global.Receive<UnlockedCellBundle>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    var popup = PopupManager.RequestSimplePopup()
                        .Initialize(_this.message.Get, _this.okMessage.Get);
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
