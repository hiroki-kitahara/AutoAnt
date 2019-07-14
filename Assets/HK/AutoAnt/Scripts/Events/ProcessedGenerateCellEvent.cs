using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Events
{
    /// <summary>
    /// セルイベントの生成処理を行った際のイベント
    /// </summary>
    public sealed class ProcessedGenerateCellEvent : Message<ProcessedGenerateCellEvent, Constants.CellEventGenerateEvalute>
    {
        /// <summary>
        /// 建設評価
        /// </summary>
        public Constants.CellEventGenerateEvalute Evalute => this.param1;
    }
}
