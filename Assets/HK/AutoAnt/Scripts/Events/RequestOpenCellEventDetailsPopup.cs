using HK.AutoAnt.CellControllers.Events;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Events
{
    /// <summary>
    /// セルイベント詳細ポップアップの表示をリクエストするイベント
    /// </summary>
    public sealed class RequestOpenCellEventDetailsPopup : Message<RequestOpenCellEventDetailsPopup, CellEvent>
    {
        /// <summary>
        /// 表示したいセルイベント
        /// </summary>
        public CellEvent CellEvent => this.param1;
    }
}
