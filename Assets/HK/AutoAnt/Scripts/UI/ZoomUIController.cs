using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UniRx;

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
                    Debug.Log(x);
                })
                .AddTo(this);
        }
    }
}
