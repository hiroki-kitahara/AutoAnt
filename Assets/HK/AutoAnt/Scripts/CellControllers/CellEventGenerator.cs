using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers
{
    /// <summary>
    /// <see cref="Cell"/>のイベントを生成する
    /// </summary>
    public sealed class CellEventGenerator
    {
        private readonly CellManager manager;

        private readonly CellSpec cellSpec;

        private readonly CellEventGenerateSpec cellEventGenerateSpec;

        private readonly CellMapper cellMapper;

        public CellEventGenerator(CellManager manager, CellSpec cellSpec, CellEventGenerateSpec cellEventGenerateSpec, CellMapper cellMapper)
        {
            this.manager = manager;
            this.cellSpec = cellSpec;
            this.cellEventGenerateSpec = cellEventGenerateSpec;
            this.cellMapper = cellMapper;

            Observable.Interval(TimeSpan.FromSeconds(this.cellEventGenerateSpec.GenerateInterval))
                .SubscribeWithState(this, (_, _this) =>
                {
                    if(_this.cellMapper.NotHasEventCellIds.Count > 0)
                    {
                        var cell = _this.cellMapper.Map[_this.cellMapper.NotHasEventCellIds[0]];
                        cell.AddEvent(_this.cellSpec.GetUnitSpec(cell.Type).ClickEvents[0]);
                    }
                })
                .AddTo(this.manager);
        }
    }
}
