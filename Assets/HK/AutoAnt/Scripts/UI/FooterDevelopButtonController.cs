using System.Linq;
using HK.AutoAnt.Events;
using HK.AutoAnt.Extensions;
using HK.AutoAnt.Systems;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// フッターメニューの開拓ボタンを制御するクラス
    /// </summary>
    public sealed class FooterDevelopButtonController : MonoBehaviour
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
                    _this.footerController.ShowCancel();
                    Broker.Global.Publish(RequestDevelopMode.Get());
                })
                .AddTo(this);
        }
    }
}
