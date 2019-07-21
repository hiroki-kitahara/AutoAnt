using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Events
{
    /// <summary>
    /// 放置時間の処理が完了した際のイベント
    /// </summary>
    public sealed class ProcessedLeftAlone : Message<ProcessedLeftAlone, double, double>
    {
        /// <summary>
        /// 獲得したお金
        /// </summary>
        public double Money => this.param1;

        /// <summary>
        /// 獲得した人口
        /// </summary>
        public double Population => this.param2;
    }
}
