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

        private readonly CellMapper cellMapper;

        private CellEvent GeneratableCellEvent => GameSystem.Instance.MasterData.CellEvent.Records.Get(this.RecordId).EventData;

        public CellEventGenerator(CellMapper cellMapper)
        {
            this.cellMapper = cellMapper;
        }

        public void Generate(Cell cell)
        {
            Assert.IsFalse(cell.HasEvent);
            cell.AddEvent(this.GeneratableCellEvent);
        }

        public void Erase(Cell cell)
        {
            Assert.IsTrue(cell.HasEvent);
            cell.ClearEvent();
        }

        /// <summary>
        /// イベントが作成可能か返す
        /// </summary>
        public bool CanGenerate(Cell cell)
        {
            return this.GeneratableCellEvent.CanGenerate(cell, this.cellMapper);
        }
    }
}
