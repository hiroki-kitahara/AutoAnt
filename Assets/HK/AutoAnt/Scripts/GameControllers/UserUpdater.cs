using System;
using System.Collections.Generic;
using HK.AutoAnt.Events;
using HK.AutoAnt.Systems;
using HK.AutoAnt.UserControllers;
using HK.Framework.EventSystems;
using HK.Framework.Text;
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
        /// 街の人口を加算する要素リスト
        /// </summary>
        private readonly List<IAddTownPopulation> addTownPopulations = new List<IAddTownPopulation>();

        void Awake()
        {
            Broker.Global.Receive<GameStart>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.RegisterUpdate();
                    _this.StartObserveUnlockCellEvents();
                })
                .AddTo(this);

            Broker.Global.Receive<GameEnd>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    GameSystem.Instance.User.History.Game.LastDateTime = DateTime.Now;
                })
                .AddTo(this);
                
            Broker.Global.Receive<GamePause>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    GameSystem.Instance.User.History.Game.LastDateTime = DateTime.Now;
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
        /// 毎フレーム実行される更新処理
        /// </summary>
        private void RegisterUpdate()
        {
            this.UpdateAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    GameSystem.Instance.User.History.Game.Time += Time.deltaTime;
                    _this.UpdateParameter(Time.deltaTime);
                })
                .AddTo(this);
        }

        /// <summary>
        /// パラメータを更新する
        /// </summary>
        public void UpdateParameter(float deltaTime)
        {
            var gameSystem = GameSystem.Instance;

            // 税金徴収
            gameSystem.User.Wallet.AddMoney(Calculator.Tax(gameSystem.User.Town.Population.Value, deltaTime));

            // 街の人口の増加
            foreach (var a in this.addTownPopulations)
            {
                a.Add(deltaTime);
            }
        }

        private void StartObserveUnlockCellEvents()
        {
            Broker.Global.Receive<AddedGenerateCellEventHistory>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    var gameSystem = GameSystem.Instance;
                    var elements = gameSystem.User.UnlockCellEvent.Elements;
                    foreach (var i in gameSystem.MasterData.UnlockCellEvent.Records)
                    {
                        // 既にアンロック済みならなにもしない
                        if (elements.Contains(i.UnlockCellEventRecordId))
                        {
                            continue;
                        }
                        var histories = gameSystem.User.History.GenerateCellEvent;
                        if (histories.IsEnough(i.NeedCellEvents))
                        {
                            elements.Add(i.UnlockCellEventRecordId);
                            Broker.Global.Publish(UnlockedCellEvent.Get(i.UnlockCellEventRecordId));
                        }
                    }
                })
                .AddTo(GameSystem.Instance);
        }
    }
}
