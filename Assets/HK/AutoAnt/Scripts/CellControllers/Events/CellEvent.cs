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
        [SerializeField]
        private CellEventGenerateCondition condition;
        
        public abstract CellGimmickController CreateGimmickController();

        public bool CanGenerate(Cell owner)
        {
            Assert.IsNotNull(this.condition);
            
            return this.condition.Evalute(owner);
        }

        public virtual void OnRegister(Cell owner)
        {
        }

        public virtual void OnClick(Cell owner)
        {
        }
    }
}
