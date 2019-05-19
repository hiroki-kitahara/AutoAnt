using HK.AutoAnt.CellControllers.ClickEvents;
using HK.AutoAnt.Constants;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers
{
    /// <summary>
    /// セルの中枢となるクラス
    /// </summary>
    public sealed class Cell : MonoBehaviour, IClickableObject
    {
        [SerializeField]
        private BoxCollider boxCollider;

        [SerializeField]
        private Transform scalableObject;

        public Vector2Int Id { get; private set; }

        public CellType Type { get; private set; }

        private ICellClickEvent clickEvent = null;

        public Transform CachedTransform{ get; private set; }

        public bool HasEvent => this.clickEvent != null;

        void Awake()
        {
            this.CachedTransform = this.transform;
        }

        public Cell Initialize(Vector2Int id, CellType cellType, CellSpec cellSpec, ICellClickEvent clickEvent)
        {
            this.Id = id;
            this.Type = cellType;
            this.clickEvent = clickEvent;

            this.CachedTransform.position = new Vector3(id.x * cellSpec.Interval, 0.0f, id.y * cellSpec.Interval);
            this.scalableObject.localScale = cellSpec.Scale;
            this.boxCollider.center = new Vector3(0.0f, cellSpec.Scale.y / 2.0f, 0.0f);
            this.boxCollider.size = cellSpec.Scale;

            return this;
        }

        public void OnClickDown()
        {
        }

        public void OnClickUp()
        {
            if(this.clickEvent == null)
            {
                return;
            }

            this.clickEvent.Do();
        }
    }
}
