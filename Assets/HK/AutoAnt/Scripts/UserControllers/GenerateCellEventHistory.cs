﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        public IReadOnlyDictionary<int, CellEvent> Histories => this.histories;
        private Dictionary<int, CellEvent> histories = new Dictionary<int, CellEvent>();

        public void AddHistory(int cellEventRecordId, int level)
        {
            if(!this.histories.ContainsKey(cellEventRecordId))
            {
                this.histories.Add(cellEventRecordId, new CellEvent());
            }

            this.histories[cellEventRecordId].Add(level);
            Debug.Log($"recordId = {cellEventRecordId}, {string.Join(",", this.histories[cellEventRecordId].Numbers.Select(s => s.ToString()))}");

            Broker.Global.Publish(AddedGenerateCellEventHistory.Get(this, cellEventRecordId));
        }

        public class CellEvent
        {
            public IReadOnlyList<int> Numbers => this.numbers;
            private List<int> numbers = new List<int>();

            public void Add(int level)
            {
                while(this.numbers.Count - 1 < level)
                {
                    this.numbers.Add(0);
                }

                this.numbers[level]++;
            }
        }
    }
}
