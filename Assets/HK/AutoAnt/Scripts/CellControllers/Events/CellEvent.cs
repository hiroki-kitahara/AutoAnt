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

        /// <summary>
        /// 必要なセルのサイズ
        /// </summary>
        [SerializeField]
        private Vector2Int size = Vector2Int.one;

        public abstract CellGimmickController CreateGimmickController();

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            var newSize = this.size;
            newSize.x = newSize.x <= 1 ? 1 : newSize.x;
            newSize.y = newSize.y <= 1 ? 1 : newSize.y;
            this.size = newSize;
        }
#endif

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
