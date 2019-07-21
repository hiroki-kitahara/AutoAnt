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
        [SerializeField]
        private StringAsset.Finder leftAloneLocalNotificationTitle = null;

        [SerializeField]
        private StringAsset.Finder leftAloneLocalNotificationMessage = null;

        /// <summary>
        /// 街の人口を加算する要素リスト
        /// </summary>
        private readonly List<IAddTownPopulation> addTownPopulations = new List<IAddTownPopulation>();

        void Awake()
        {
            Broker.Global.Receive<GameStart>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.RegisterUpdate(x.GameSystem);
                    _this.StartObserveUnlockCellEvents(x.GameSystem);
                })
                .AddTo(this);

            Broker.Global.Receive<GameEnd>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    x.GameSystem.User.History.Game.LastDateTime = DateTime.Now;
                    // _this.RegisterLeftAloneLocalNotification();
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
        private void RegisterUpdate(GameSystem gameSystem)
        {
            this.UpdateAsObservable()
                .SubscribeWithState2(this, gameSystem, (_, _this, _gameSystem) =>
                {
                    _gameSystem.User.History.Game.Time += Time.deltaTime;
                    _this.UpdateParameter(_gameSystem);
                })
                .AddTo(this);
        }

        /// <summary>
        /// パラメータを更新する
        /// </summary>
        public void UpdateParameter(GameSystem gameSystem)
        {
            // 税金徴収
            gameSystem.User.Wallet.AddMoney(Calculator.Tax(gameSystem.User.Town.Population.Value, Time.deltaTime));

            // 街の人口の増加
            foreach (var a in this.addTownPopulations)
            {
                a.Add(gameSystem, Time.deltaTime);
            }
        }

        private void StartObserveUnlockCellEvents(GameSystem gameSystem)
        {
            Broker.Global.Receive<AddedGenerateCellEventHistory>()
                .SubscribeWithState2(this, gameSystem, (_, _this, _gameSystem) =>
                {
                    var elements = _gameSystem.User.UnlockCellEvent.Elements;
                    foreach (var i in _gameSystem.MasterData.UnlockCellEvent.Records)
                    {
                        // 既にアンロック済みならなにもしない
                        if (elements.Contains(i.UnlockCellEventRecordId))
                        {
                            continue;
                        }
                        var histories = _gameSystem.User.History.GenerateCellEvent;
                        if (histories.IsEnough(i.NeedCellEvents))
                        {
                            elements.Add(i.UnlockCellEventRecordId);
                            Broker.Global.Publish(UnlockedCellEvent.Get(i.UnlockCellEventRecordId));
                        }
                    }
                })
                .AddTo(gameSystem);
        }

        /// <summary>
        /// 放置可能な時間後にローカル通知を登録する
        /// </summary>
        // private void RegisterLeftAloneLocalNotification()
        // {
        //     AutoAntSystem.LocalNotification.Register(
        //         this.leftAloneLocalNotificationTitle.Get,
        //         this.leftAloneLocalNotificationMessage.Get,
        //         (int)this.leftAloneProcessSeconds
        //     );
        // }
    }
}
