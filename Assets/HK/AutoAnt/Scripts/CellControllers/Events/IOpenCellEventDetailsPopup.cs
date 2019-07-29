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
        /// <summary>
        /// アタッチする
        /// </summary>
        void Attach(CellEventDetailsPopup popup);

        /// <summary>
        /// ポップアップを更新する
        /// </summary>
        void Update(CellEventDetailsPopup popup);
    }
}
