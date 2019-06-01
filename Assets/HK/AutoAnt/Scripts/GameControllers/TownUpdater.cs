using System;
using System.Collections.Generic;
using HK.AutoAnt.UserControllers;
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

        /// <summary>
        /// 街の人口を加算する要素リスト
        /// </summary>
        public readonly List<IAddTownPopulation> AddTownPopulations = new List<IAddTownPopulation>();

        public void Initialize(User user, GameObject owner)
        {
            Observable.Interval(TimeSpan.FromSeconds(this.parameterUpdateInterval))
                .SubscribeWithState2(this, user, (_, _this, _user) =>
                {
                    foreach(var a in _this.AddTownPopulations)
                    {
                        _user.Town.AddPopulation(a.GetAmount(_user.Town));
                    }
                })
                .AddTo(owner);
        }
    }
}
