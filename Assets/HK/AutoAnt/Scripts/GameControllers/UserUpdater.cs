using System;
using System.Collections.Generic;
using HK.AutoAnt.UserControllers;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// ユーザーデータを更新する
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/TownUpdater")]
    public sealed class UserUpdater : ScriptableObject
    {
        /// <summary>
        /// 各パラメータの更新を行う間隔（秒）
        /// </summary>
        [SerializeField]
        private float parameterUpdateInterval = 1.0f;

        /// <summary>
        /// 街の人口を加算する要素リスト
        /// </summary>
        public readonly List<IAddTownPopulation> AddTownPopulations = new List<IAddTownPopulation>();

        public void Initialize(User user, GameObject owner)
        {
            Observable.Interval(TimeSpan.FromSeconds(this.parameterUpdateInterval))
                .SubscribeWithState2(this, user, (_, _this, _user) =>
                {
                    // 税金徴収
                    // FIXME: 税金計算を実装する
                    _user.Wallet.AddMoney(_user.Town.Population.Value * 10);

                    // 街の人口の増加
                    foreach(var a in _this.AddTownPopulations)
                    {
                        a.Add(_user.Town);
                    }
                })
                .AddTo(owner);
        }
    }
}
