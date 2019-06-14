using System;
using System.Collections.Generic;
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

            Debug.Log($"celEventRecordId = {cellEventRecordId}, value = {this.histories[cellEventRecordId]}");
        }
    }
}
