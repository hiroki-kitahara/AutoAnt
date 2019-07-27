using HK.AutoAnt.Events;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// オプションを開くボタンを制御するクラス
    /// </summary>
    public sealed class OpenOptionButtonController : MonoBehaviour
    {
        [SerializeField]
        private Button button = null;

        void Awake()
        {
            this.button.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    Broker.Global.Publish(RequestOpenOptionPopup.Get());
                })
                .AddTo(this);
        }
    }
}
