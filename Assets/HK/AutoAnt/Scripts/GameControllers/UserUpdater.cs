using System;
using System.Collections.Generic;
using HK.AutoAnt.Events;
using HK.AutoAnt.Systems;
using HK.AutoAnt.UserControllers;
using HK.Framework.EventSystems;
using UniRx;
using UniRx.Triggers;
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
        private readonly List<IAddTownPopulation> addTownPopulations = new List<IAddTownPopulation>();

        void Awake()
        {
            Broker.Global.Receive<GameStart>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.RegisterIntervalUpdate(x.GameSystem);
                    _this.RegisterUpdate(x.GameSystem);
                })
                .AddTo(this);

            Broker.Global.Receive<GameEnd>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    x.GameSystem.User.History.Game.LastDateTime = DateTime.Now;
                })
                .AddTo(this);

            Broker.Global.Receive<AddedCellEvent>()
                .Where(x => x.CellEvent is IAddTownPopulation)
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.addTownPopulations.Add(x.CellEvent as IAddTownPopulation);
                })
                .AddTo(this);

            Broker.Global.Receive<RemovedCellEvent>()
                .Where(x => x.CellEvent is IAddTownPopulation)
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.addTownPopulations.Remove(x.CellEvent as IAddTownPopulation);
                })
                .AddTo(this);
        }

        /// <summary>
        /// パラメータ更新処理
        /// </summary>
        private void RegisterIntervalUpdate(GameSystem gameSystem)
        {
            Observable.Interval(TimeSpan.FromSeconds(this.parameterUpdateInterval))
                .SubscribeWithState2(this, gameSystem, (_, _this, _gameSystem) =>
                {
                    // 税金徴収
                    // FIXME: 税金計算を実装する
                    _gameSystem.User.Wallet.AddMoney(_gameSystem.User.Town.Population.Value * 10);

                    // 街の人口の増加
                    foreach (var a in _this.addTownPopulations)
                    {
                        a.Add(_gameSystem);
                    }
                })
                .AddTo(this);
        }

        /// <summary>
        /// 毎フレーム実行される更新処理
        /// </summary>
        private void RegisterUpdate(GameSystem gameSystem)
        {
            this.UpdateAsObservable()
                .SubscribeWithState2(this, gameSystem, (_, _this, _gameSystem) =>
                {
                    _gameSystem.User.History.Game.Time += Time.deltaTime;
                })
                .AddTo(this);
        }
    }
}
