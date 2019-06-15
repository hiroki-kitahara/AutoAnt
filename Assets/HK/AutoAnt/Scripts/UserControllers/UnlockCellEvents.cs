using System.Collections.Generic;
using HK.AutoAnt.Events;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UserControllers
{
    /// <summary>
    /// 生成可能なセルイベントを管理するクラス
    /// </summary>
    public sealed class UnlockCellEvents
    {
        /// <summary>
        /// 生成可能なセルイベントリスト
        /// </summary>
        public IReadOnlyList<int> CellEvents => this.cellEvents;
        private List<int> cellEvents = new List<int>();

        /// <summary>
        /// 生成履歴の監視を開始する
        /// </summary>
        public void StartObserve()
        {
            // Broker.Global.Receive<AddedGenerateCellEventHistory>()
            //     .SubscribeWithState()
        }
    }
}
