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

        private readonly CellEventGenerateSpec spec;

        public CellEventGenerator(CellManager manager, CellEventGenerateSpec spec)
        {
            this.manager = manager;
            this.spec = spec;
            Observable.Interval(TimeSpan.FromSeconds(this.spec.GenerateInterval))
                .SubscribeWithState(this, (_, _this) =>
                {
                })
                .AddTo(this.manager);
        }
    }
}
