using UnityEngine;
using UnityEngine.Assertions;
using HK.AutoAnt.CellControllers;
using HK.Framework.EventSystems;
using HK.AutoAnt.CellControllers.Events;

namespace HK.AutoAnt.Events
{
    /// <summary>
    /// セルのイベントが削除された際のイベント
    /// </summary>
    public sealed class RemovedCellEvent : Message<RemovedCellEvent, ICellEvent>
    {
        public ICellEvent CellEvent => this.param1;
    }
}
