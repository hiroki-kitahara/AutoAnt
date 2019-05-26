using HK.AutoAnt.CellControllers.Gimmicks;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers.Events
{
    /// <summary>
    /// <see cref="Cell"/>の各種イベントを持つインターフェイス
    /// </summary>
    public interface ICellEvent
    {
        /// <summary>
        /// <see cref="CellGimmickController"/>を生成する
        /// </summary>
        CellGimmickController CreateGimmickController();

        /// <summary>
        /// 作成可能か返す
        /// </summary>
        bool CanGenerate(Cell owner);

        /// <summary>
        /// イベントがセルに登録された時の処理
        /// </summary>
        void OnRegister(Cell owner);

        /// <summary>
        /// セルがクリックされた時の処理
        /// </summary>
        void OnClick(Cell owner);
    }
}
