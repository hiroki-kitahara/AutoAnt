using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Events
{
    /// <summary>
    /// 利用可能なセルイベントがアンロックされた際のイベント
    /// </summary>
    public sealed class UnlockedCellEvent : Message<UnlockedCellEvent, int>
    {
        /// <summary>
        /// アンロックされたセルイベントのレコードID
        /// </summary>
        public int CellEventRecordId => this.param1;
    }
}
