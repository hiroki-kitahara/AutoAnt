using System.Collections.Generic;
using HK.AutoAnt.Database;
using HK.AutoAnt.Events;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UserControllers
{
    /// <summary>
    /// セルイベントの生成履歴を保持するクラス
    /// </summary>
    public sealed class GenerateCellEventHistory
    {
        /// <summary>
        /// 生成履歴
        /// key = cellEventRecordId
        /// value = 生成した数
        /// </summary>
        public IReadOnlyDictionary<int, GenerateCellEventHistoryElement> Elements => this.elements;
        private Dictionary<int, GenerateCellEventHistoryElement> elements = new Dictionary<int, GenerateCellEventHistoryElement>();

        public void AddHistory(int cellEventRecordId, int level)
        {
            if (!this.elements.ContainsKey(cellEventRecordId))
            {
                this.elements.Add(cellEventRecordId, new GenerateCellEventHistoryElement());
            }

            this.elements[cellEventRecordId].Add(level);

            Broker.Global.Publish(AddedGenerateCellEventHistory.Get(this, cellEventRecordId));
        }

        /// <summary>
        /// アンロック可能か返す
        /// </summary>
        public bool IsEnough(MasterDataUnlockCellEvent.Record.NeedCellEvent[] needs)
        {
            foreach (var n in needs)
            {
                // そもそも生成履歴に存在しない場合はアンロック出来ない
                if (!elements.ContainsKey(n.CellEventRecordId))
                {
                    return false;
                }

                if (!elements[n.CellEventRecordId].IsEnough(n))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
