using System;
using System.Collections.Generic;
using System.Linq;
using HK.AutoAnt.Database;
using HK.AutoAnt.Events;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UserControllers
{
    /// <summary>
    /// ユーザーの行動履歴
    /// </summary>
    [Serializable]
    public sealed class History
    {
        /// <summary>
        /// 生成履歴
        /// key = cellEventRecordId
        /// value = 生成した数
        /// </summary>
        public IReadOnlyDictionary<int, CellEvent> GenerateCellEvent => this.generateCellEvent;
        private Dictionary<int, CellEvent> generateCellEvent = new Dictionary<int, CellEvent>();

        public void AddHistory(int cellEventRecordId, int level)
        {
            if(!this.generateCellEvent.ContainsKey(cellEventRecordId))
            {
                this.generateCellEvent.Add(cellEventRecordId, new CellEvent());
            }

            this.generateCellEvent[cellEventRecordId].Add(level);

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
                if (!generateCellEvent.ContainsKey(n.CellEventRecordId))
                {
                    return false;
                }

                if(!generateCellEvent[n.CellEventRecordId].IsEnough(n))
                {
                    return false;
                }
            }

            return true;
        }

        public class CellEvent
        {
            /// <summary>
            /// 建設した数
            /// </summary>
            /// <remarks>
            /// レベルごとに建設した数を保持しています
            /// [0]はレベル1の建設した数になります
            /// </remarks>
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

            /// <summary>
            /// アンロック可能か返す
            /// </summary>
            public bool IsEnough(MasterDataUnlockCellEvent.Record.NeedCellEvent need)
            {
                // 指定されたレベルを一度も生成したことが無い場合はアンロック出来ない
                if(this.numbers.Count < need.Level)
                {
                    return false;
                }

                return this.numbers[need.Level - 1] >= need.Number;
            }
        }
    }
}
