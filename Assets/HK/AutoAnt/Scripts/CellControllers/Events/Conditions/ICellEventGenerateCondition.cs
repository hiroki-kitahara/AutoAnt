using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers.Events
{
    /// <summary>
    /// セルイベントの作成可能な条件を持つインターフェイス
    /// </summary>
    public interface ICellEventGenerateCondition
    {
        /// <summary>
        /// 作成可能か返す
        /// </summary>
        bool Evalute(Cell cell);
    }
}
