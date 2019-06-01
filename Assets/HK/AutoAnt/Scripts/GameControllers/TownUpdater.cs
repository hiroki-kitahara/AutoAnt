using System;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// 街データを更新する
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/TownUpdater")]
    public sealed class TownUpdater : ScriptableObject
    {
        /// <summary>
        /// 各パラメータの更新を行う間隔（秒）
        /// </summary>
        [SerializeField]
        private float parameterUpdateInterval;

        public void Initialize(GameObject owner)
        {
            Observable.Interval(TimeSpan.FromSeconds(this.parameterUpdateInterval))
                .SubscribeWithState(this, (_, _this) =>
                {
                    Debug.Log("TownUpdater");
                })
                .AddTo(owner);
        }
    }
}
