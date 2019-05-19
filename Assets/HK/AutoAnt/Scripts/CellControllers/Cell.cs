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

        private CellMapper cellMapper;

        public Transform CachedTransform{ get; private set; }

        private GameObject clickEventEffect;

        private CellSpec cellSpec;

        public bool HasEvent => this.clickEvent != null;

        void Awake()
        {
            this.CachedTransform = this.transform;
        }

        public Cell Initialize(Vector2Int id, CellType cellType, CellSpec cellSpec, ICellClickEvent clickEvent, CellMapper cellMapper)
        {
            this.Id = id;
            this.Type = cellType;
            this.clickEvent = clickEvent;
            this.cellMapper = cellMapper;
            this.cellSpec = cellSpec;

            this.CachedTransform.position = new Vector3(id.x * cellSpec.Interval, 0.0f, id.y * cellSpec.Interval);
            this.scalableObject.localScale = cellSpec.Scale;
            this.boxCollider.center = new Vector3(0.0f, cellSpec.Scale.y / 2.0f, 0.0f);
            this.boxCollider.size = cellSpec.Scale;

            return this;
        }

        public void AddEvent(ICellClickEvent clickEvent)
        {
            this.clickEvent = clickEvent;
            if(this.clickEvent != null)
            {
                this.cellMapper.RegisterHasEvent(this);
                this.clickEventEffect = Instantiate(this.clickEvent.Prefab);
                this.clickEventEffect.transform.SetParent(this.CachedTransform);
                this.clickEventEffect.transform.localPosition = new Vector3(0.0f, this.cellSpec.Scale.y, 0.0f);
            }
            else
            {
                this.cellMapper.RegisterNotHasEvent(this);
                Destroy(this.clickEventEffect);
            }
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

            this.clickEvent.Do(this);
        }
    }
}
