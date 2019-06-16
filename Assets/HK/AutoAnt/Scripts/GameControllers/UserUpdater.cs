using System;
using System.Collections.Generic;
using HK.AutoAnt.Events;
using HK.AutoAnt.UserControllers;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// ユーザーデータを更新する
    /// </summary>
    public sealed class UserUpdater : MonoBehaviour
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

        void Awake()
        {
            Broker.Global.Receive<GameStart>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.RegisterIntervalUpdate(x.GameSystem.User);
                })
                .AddTo(this);

            Broker.Global.Receive<AddedCellEvent>()
                .Where(x => x.CellEvent is IAddTownPopulation)
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.AddTownPopulations.Add(x.CellEvent as IAddTownPopulation);
                })
                .AddTo(this);

            Broker.Global.Receive<ReleasedCellEvent>()
                .Where(x => x.CellEvent is IAddTownPopulation)
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.AddTownPopulations.Remove(x.CellEvent as IAddTownPopulation);
                })
                .AddTo(this);
        }

        private void RegisterIntervalUpdate(User user)
        {
            Observable.Interval(TimeSpan.FromSeconds(this.parameterUpdateInterval))
                .SubscribeWithState2(this, user, (_, _this, _user) =>
                {
                    // 税金徴収
                    // FIXME: 税金計算を実装する
                    _user.Wallet.AddMoney(_user.Town.Population.Value * 10);

                    // 街の人口の増加
                    foreach (var a in _this.AddTownPopulations)
                    {
                        a.Add(_user.Town);
                    }
                })
                .AddTo(this);
        }
    }
}
