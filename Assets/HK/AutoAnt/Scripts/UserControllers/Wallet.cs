using System;
using HK.AutoAnt.SaveData.Serializables;
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
        public double Money => this.money.Value;

        public ReactiveProperty<double> MoneyAsObservable => this.money;

        [SerializeField]
        private DoubleReactiveProperty money = new DoubleReactiveProperty();

        /// <summary>
        /// お金が足りているか返す
        /// </summary>
        public bool IsEnoughMoney(double value)
        {
            return this.Money >= value;
        }

        /// <summary>
        /// お金を加算する
        /// </summary>
        public void AddMoney(double value)
        {
            this.money.Value += value;
            Assert.IsTrue(this.Money >= 0);
        }

        public SerializableWallet GetSerializable()
        {
            return new SerializableWallet()
            {
                Money = this.Money
            };
        }

        public void Deserialize(SerializableWallet serializableData)
        {
            this.money.Value = serializableData.Money;
        }
#if AA_DEBUG
        /// <summary>
        /// お金を設定する
        /// </summary>
        public void SetMoney(double value)
        {
            this.money.Value = value;
        }
#endif
    }
}
