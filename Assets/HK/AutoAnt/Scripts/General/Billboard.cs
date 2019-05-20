using HK.AutoAnt.Systems;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt
{
    /// <summary>
    /// カメラマンの方へ常に向くコンポーネント
    /// </summary>
    public sealed class Billboard : MonoBehaviour
    {
        [SerializeField]
        private bool isReverse;

        private Transform cachedTransform;

        void Awake()
        {
            this.cachedTransform = this.transform;

            this.LateUpdateAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    var forward = -GameSystem.Instance.Cameraman.Camera.transform.forward;
                    if(_this.isReverse)
                    {
                        forward *= -1.0f;
                    }

                    _this.cachedTransform.forward = forward;
                });
        }
    }
}
