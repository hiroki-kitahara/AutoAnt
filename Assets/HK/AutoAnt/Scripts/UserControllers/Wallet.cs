using System;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UserControllers
{
    /// <summary>
    /// 財布を管理するクラス
    /// </summary>
    [Serializable]
    public sealed class Wallet
    {
        /// <summary>
        /// お金
        /// </summary>
        public int Money => this.money.Value;

        public IReactiveProperty<int> MoneyAsObservable => this.money;

        [SerializeField]
        private IntReactiveProperty money = new IntReactiveProperty();

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
            this.money.Value += value;
            Assert.IsTrue(this.Money >= 0);
        }
    }
}
