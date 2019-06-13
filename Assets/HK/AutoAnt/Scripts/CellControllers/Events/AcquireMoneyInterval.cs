using System;
using HK.AutoAnt.CellControllers.Gimmicks;
using HK.AutoAnt.Systems;
using HK.Framework.Text;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers.Events
{
    /// <summary>
    /// 一定間隔でお金を取得するセルイベント
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Cell/Event/AcquireMoneyInterval")]
    public sealed class AcquireMoneyInterval : CellEventBlankGimmick
    {
        /// <summary>
        /// 取得するまでの時間
        /// </summary>
        [SerializeField]
        private float intervalSeconds = 1.0f;

        /// <summary>
        /// 取得できる量
        /// </summary>
        [SerializeField]
        private int amount = 0;

        public override void Initialize(Vector2Int position, GameSystem gameSystem, bool isInitializingGame)
        {
            base.Initialize(position, gameSystem, isInitializingGame);

            Observable.Interval(TimeSpan.FromSeconds(this.intervalSeconds))
                .SubscribeWithState(this, (_, _this) =>
                {
                    GameSystem.Instance.User.Wallet.AddMoney(_this.amount);
                })
                .AddTo(this.instanceEvents);
        }
    }
}
