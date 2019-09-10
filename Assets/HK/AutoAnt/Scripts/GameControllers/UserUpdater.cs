using System;
using System.Collections.Generic;
using System.Linq;
using HK.AutoAnt.CellControllers;
using HK.AutoAnt.Events;
using HK.AutoAnt.Extensions;
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
        private CellManager cellManager = null;
        
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
                    _this.CheckUnlockCellBundle();
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

        /// <summary>
        /// CellBundleのアンロックが行えるかチェックする
        /// </summary>
        /// <remarks>
        /// 人口数によってアンロック出来るか確認するので毎フレーム実行しています
        /// </remarks>
        private void CheckUnlockCellBundle()
        {
            var user = GameSystem.Instance.User;
            if(!user.UnlockCellBundle.CanUnlock)
            {
                return;
            }
            
            if(user.UnlockCellBundle.NextPopulation <= user.Town.Population.Value)
            {
                var masterData = GameSystem.Instance.MasterData.UnlockCellBundle;
                var unlockCellBundles = user.UnlockCellBundle.TargetRecordIds
                    .Select(id => masterData.Records.Get(id));

                foreach(var r in unlockCellBundles)
                {
                    this.cellManager.CellGenerator.GenerateFromCellBundle(r.UnlockCellBundleGroup);
                }

                user.UnlockCellBundle.SetNextPopulation(masterData);
            }
        }
    }
}
