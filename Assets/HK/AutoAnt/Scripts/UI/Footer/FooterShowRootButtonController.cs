using HK.AutoAnt.Events;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// フッターメニューのルートを表示するボタンを制御するクラス
    /// </summary>
    public sealed class FooterShowRootButtonController : MonoBehaviour
    {
        [SerializeField]
        private Button button = null;

        [SerializeField]
        private FooterController footerController = null;

        void Awake()
        {
            this.button.OnClickAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.footerController.ShowRoot();
                    Broker.Global.Publish(RequestClickMode.Get());
                })
                .AddTo(this);
        }
    }
}
