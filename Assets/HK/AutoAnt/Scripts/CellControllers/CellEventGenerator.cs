using System;
using HK.AutoAnt.CellControllers.Events;
using HK.AutoAnt.Extensions;
using HK.AutoAnt.GameControllers;
using HK.AutoAnt.Systems;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers
{
    /// <summary>
    /// <see cref="Cell"/>のイベントを生成する
    /// </summary>
    public sealed class CellEventGenerator : IMasterDataCellEventRecordIdHolder
    {
        /// <summary>
        /// 作成可能なセルイベントのレコードID
        /// </summary>
        public int RecordId { get; set; } = 100000;

        public void Generate(Cell cell)
        {
            Assert.IsFalse(cell.HasEvent);
            var cellEvent = GameSystem.Instance.MasterData.CellEvent.Records.Get(this.RecordId).EventData;
            cell.AddEvent(cellEvent);
        }

        public void Erase(Cell cell)
        {
            Assert.IsTrue(cell.HasEvent);
            cell.ClearEvent();
        }
    }
}
