using UnityEngine;
using UnityEngine.Assertions;
using HK.AutoAnt.UI;

namespace HK.AutoAnt.CellControllers.Events
{
    /// <summary>
    /// <see cref="FooterSelectCellEventController"/>で表示可能なセルイベントのインターフェイス
    /// </summary>
    public interface IFooterSelectCellEvent : ICellEvent
    {
        void Attach(FooterSelectCellEventController controller);
    }
}
