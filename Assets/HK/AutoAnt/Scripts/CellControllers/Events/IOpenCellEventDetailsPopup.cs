using UnityEngine;
using UnityEngine.Assertions;
using HK.AutoAnt.UI;

namespace HK.AutoAnt.CellControllers.Events
{
    /// <summary>
    /// <see cref="CellEventDetailsPopup"/>を開くことが出来るセルイベントのインターフェイス
    /// </summary>
    public interface IOpenCellEventDetailsPopup
    {
        void Attach(CellEventDetailsPopup popup);

        void Update(CellEventDetailsPopup popup);
    }
}
