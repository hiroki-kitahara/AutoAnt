using HK.AutoAnt.CellControllers.Events;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Events
{
    /// <summary>
    /// セルイベントが追加された際のイベント
    /// </summary>
    public sealed class AddedCellEvent : Message<AddedCellEvent, ICellEvent>
    {
        public ICellEvent CellEvent => this.param1;
    }
}
