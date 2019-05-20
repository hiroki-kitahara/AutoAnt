using HK.AutoAnt.CellControllers.Gimmicks;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers.ClickEvents
{
    /// <summary>
    /// <see cref="Cell"/>をクリックした際のイベントのインターフェイス
    /// </summary>
    public interface ICellClickEvent
    {
        /// <summary>
        /// <see cref="CellGimmickController"/>を生成する
        /// </summary>
        /// <returns></returns>
        CellGimmickController CreateGimmickController();

        /// <summary>
        /// イベントを実行する
        /// </summary>
        void Do(Cell owner);
    }
}
