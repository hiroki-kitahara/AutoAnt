using UnityEngine;
using UnityEngine.Assertions;
using HK.AutoAnt.CellControllers;
using HK.Framework.EventSystems;
using HK.AutoAnt.CellControllers.Events;

namespace HK.AutoAnt.Events
{
    /// <summary>
    /// セルのイベントが解放された際のイベント
    /// </summary>
    public sealed class ReleasedCellEvent : Message<ReleasedCellEvent, ICellEvent>
    {
        public ICellEvent CellEvent => this.param1;
    }
}
