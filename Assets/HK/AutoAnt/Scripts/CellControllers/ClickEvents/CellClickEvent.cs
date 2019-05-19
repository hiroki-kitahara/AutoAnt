using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers.ClickEvents
{
    /// <summary>
    /// <see cref="Cell"/>をクリックされた際のイベント抽象クラス
    /// </summary>
    public abstract class CellClickEvent : ScriptableObject, ICellClickEvent
    {
        [SerializeField]
        private GameObject prefab;
        public GameObject Prefab => this.prefab;
        
        public abstract void Do(Cell owner);
    }
}
