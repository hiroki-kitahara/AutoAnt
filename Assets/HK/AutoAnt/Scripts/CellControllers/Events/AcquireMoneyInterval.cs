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
        private float intervalSeconds;

        /// <summary>
        /// 取得できる量
        /// </summary>
        [SerializeField]
        private int amount;

        public override void OnRegister(Cell owner)
        {
            Observable.Interval(TimeSpan.FromSeconds(this.intervalSeconds))
                .TakeUntil(owner.ReleasedCellEventAsObservable())
                .SubscribeWithState(this, (_, _this) =>
                {
                    GameSystem.Instance.User.Wallet.AddMoney(_this.amount);
                })
                .AddTo(owner);
        }
    }
}
