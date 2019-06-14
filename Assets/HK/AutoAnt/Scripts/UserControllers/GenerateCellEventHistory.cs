using System;
using System.Collections.Generic;
using HK.AutoAnt.Events;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UserControllers
{
    /// <summary>
    /// セルイベントを生成した履歴
    /// </summary>
    [Serializable]
    public sealed class GenerateCellEventHistory
    {
        /// <summary>
        /// 生成履歴
        /// key = cellEventRecordId
        /// value = 生成した数
        /// </summary>
        public IReadOnlyDictionary<int, int> Histories => this.histories;
        private Dictionary<int, int> histories = new Dictionary<int, int>();

        public void AddHistory(int cellEventRecordId)
        {
            if(!this.histories.ContainsKey(cellEventRecordId))
            {
                this.histories.Add(cellEventRecordId, 1);
            }
            else
            {
                this.histories[cellEventRecordId]++;
            }

            Broker.Global.Publish(AddedGenerateCellEventHistory.Get(this, cellEventRecordId));
        }
    }
}
