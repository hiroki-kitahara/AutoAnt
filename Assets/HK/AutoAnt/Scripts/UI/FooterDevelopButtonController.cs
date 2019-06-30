using System.Linq;
using HK.AutoAnt.Extensions;
using HK.AutoAnt.Systems;
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
                })
                .AddTo(this);
        }
    }
}
