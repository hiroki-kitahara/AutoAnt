using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers.Events
{
    /// <summary>
    /// セルイベントの作成可能な条件を持つ抽象クラス
    /// </summary>
    public abstract class CellEventGenerateCondition : ScriptableObject, ICellEventGenerateCondition
    {
        public abstract bool Evalute(Cell[] cells);
    }
}
