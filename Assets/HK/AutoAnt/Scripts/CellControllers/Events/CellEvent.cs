using HK.AutoAnt.CellControllers.Gimmicks;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers.Events
{
    /// <summary>
    /// <see cref="Cell"/>のイベントを持つ抽象クラス
    /// </summary>
    public abstract class CellEvent : ScriptableObject, ICellEvent
    {
        public abstract CellGimmickController CreateGimmickController();

        public virtual void OnRegister(Cell owner)
        {
        }

        public virtual void OnClick(Cell owner)
        {
        }
    }
}
