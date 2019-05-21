using UnityEngine;
using UnityEngine.Assertions;
using HK.AutoAnt.CellControllers;
using HK.Framework.EventSystems;

namespace HK.AutoAnt.Events
{
    /// <summary>
    /// セルのイベントが解放された際のイベント
    /// </summary>
    /// <remarks>
    /// <see cref="Cell.Broker"/>に対して発行されます
    /// </remarks>
    public sealed class ReleasedCellEvent : Message<ReleasedCellEvent>
    {
    }
}
