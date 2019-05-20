using HK.AutoAnt.CellControllers.Gimmicks;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers.ClickEvents
{
    /// <summary>
    /// <see cref="Cell"/>のイベントを持つ抽象クラス
    /// </summary>
    public abstract class CellEvent : ScriptableObject, ICellEvent
    {
        [SerializeField]
        private CellGimmickController gimmickPrefab;

        public CellGimmickController CreateGimmickController()
        {
            return Instantiate(this.gimmickPrefab).Initialize();
        }

        public abstract void OnClick(Cell owner);
    }
}
