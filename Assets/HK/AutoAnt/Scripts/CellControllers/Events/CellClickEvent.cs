using HK.AutoAnt.CellControllers.Gimmicks;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers.ClickEvents
{
    /// <summary>
    /// <see cref="Cell"/>をクリックされた際のイベント抽象クラス
    /// </summary>
    public abstract class CellClickEvent : ScriptableObject, ICellEvent
    {
        [SerializeField]
        private CellGimmickController gimmickPrefab;

        public CellGimmickController CreateGimmickController()
        {
            return Instantiate(this.gimmickPrefab);
        }

        public abstract void OnClick(Cell owner);
    }
}
