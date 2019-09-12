using System.Collections.Generic;
using HK.AutoAnt.Database;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Events
{
    /// <summary>
    /// <see cref="CellBundle"/>がアンロックされた際のイベント
    /// </summary>
    public sealed class UnlockedCellBundle : Message<UnlockedCellBundle, double, List<MasterDataUnlockCellBundle.Record>>
    {
        /// <summary>
        /// 達成した人口数
        /// </summary>
        public double Population => this.param1;

        /// <summary>
        /// アンロックされた<see cref="CellBundle"/>
        /// </summary>
        public List<MasterDataUnlockCellBundle.Record> UnlockCellBundleRecords => this.param2;
    }
}
