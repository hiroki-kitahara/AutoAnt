using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UniRx;
using HK.Framework.EventSystems;
using HK.AutoAnt.Events;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// ズームUIを制御するクラス
    /// </summary>
    public sealed class ZoomUIController : MonoBehaviour
    {
        [SerializeField]
        private Slider slider = null;

        void Start()
        {
            this.slider.OnValueChangedAsObservable()
                .SubscribeWithState(this, (x, _this) =>
                {
                    Broker.Global.Publish(RequestCameraZoom.Get(x));
                })
                .AddTo(this);
        }
    }
}
