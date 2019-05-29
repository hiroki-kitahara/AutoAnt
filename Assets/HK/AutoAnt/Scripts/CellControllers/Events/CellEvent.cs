using System;
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

        public bool CanGenerate(Cell origin, CellMapper cellMapper)
        {
            Assert.IsNotNull(this.condition);

            var cellPositions = cellMapper.GetRange(origin.Position, this.size, p => cellMapper.Map.ContainsKey(p));

            // 配置したいところにセルがない場合は生成できない
            if(cellPositions.Length != this.TotalSize)
            {
                return false;
            }

            // 配置したいセルにイベントがあった場合は生成できない
            var cells = cellMapper.GetCells(cellPositions);
            if(Array.FindIndex(cells, c => c.HasEvent) != -1)
            {
                return false;
            }

            return this.condition.Evalute(cells);
        }

        public virtual void OnRegister(Cell owner)
        {
        }

        public virtual void OnClick(Cell owner)
        {
        }

        private int TotalSize
        {
            get
            {
                if(this.size == Vector2Int.one)
                {
                    return 1;
                }

                var x = this.size.x <= 1 ? 0 : this.size.x;
                var y = this.size.y <= 1 ? 0 : this.size.y;

                return x + y;
            }
        }
    }
}
