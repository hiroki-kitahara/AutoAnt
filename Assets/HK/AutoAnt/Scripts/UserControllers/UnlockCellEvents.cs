using System;
using System.Collections.Generic;
using HK.AutoAnt.Events;
using HK.AutoAnt.Systems;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UserControllers
{
    /// <summary>
    /// 生成可能なセルイベントを管理するクラス
    /// </summary>
    [Serializable]
    public sealed class UnlockCellEvents
    {
        /// <summary>
        /// 生成可能なセルイベントリスト
        /// </summary>
        public IReadOnlyList<int> CellEvents => this.cellEvents;
        private List<int> cellEvents = new List<int>();

        /// <summary>
        /// 生成履歴の監視を開始する
        /// </summary>
        public void StartObserve(GameSystem gameSystem)
        {
            Broker.Global.Receive<AddedGenerateCellEventHistory>()
                .SubscribeWithState2(this, gameSystem, (_, _this, _gameSystem) =>
                {
                    foreach(var i in _gameSystem.MasterData.UnlockCellEvent.Records)
                    {
                        // 既にアンロック済みならなにもしない
                        if(_this.cellEvents.Contains(i.UnlockCellEventRecordId))
                        {
                            continue;
                        }
                        var histories = _gameSystem.User.History;
                        if(histories.IsEnough(i.NeedCellEvents))
                        {
                            _this.cellEvents.Add(i.UnlockCellEventRecordId);
                            Broker.Global.Publish(UnlockedCellEvent.Get(i.UnlockCellEventRecordId));
                        }
                    }
                })
                .AddTo(gameSystem);
        }
    }
}
