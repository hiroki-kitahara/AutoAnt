using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UserControllers
{
    /// <summary>
    /// 財布を管理するクラス
    /// </summary>
    public sealed class Wallet
    {
        /// <summary>
        /// お金
        /// </summary>
        public int Money => this.money.Value;

        public IReactiveProperty<int> MoneyAsObservable => this.money;

        private readonly ReactiveProperty<int> money = new ReactiveProperty<int>();

        /// <summary>
        /// お金が足りているか返す
        /// </summary>
        public bool IsEnoughMoney(int value)
        {
            return this.Money >= value;
        }

        /// <summary>
        /// お金を加算する
        /// </summary>
        public void AddMoney(int value)
        {
            Assert.IsTrue(this.IsEnoughMoney(value));
            this.money.Value += value;
        }
    }
}
