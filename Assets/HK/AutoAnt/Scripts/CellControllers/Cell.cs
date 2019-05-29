using System;
using HK.AutoAnt.CellControllers.Events;
using HK.AutoAnt.CellControllers.Gimmicks;
using HK.AutoAnt.Constants;
using HK.AutoAnt.Events;
using HK.AutoAnt.Systems;
using UniRx;
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
        private BoxCollider boxCollider = null;

        [SerializeField]
        private Transform scalableObject = null;

        public Vector2Int Position { get; private set; }

        public CellType Type { get; private set; }

        private readonly IMessageBroker Broker = new MessageBroker();

        private ICellEvent cellEvent = null;

        private CellMapper cellMapper;

        public Transform CachedTransform{ get; private set; }

        private CellGimmickController gimmickController;

        public bool HasEvent => this.cellEvent != null;

        void Awake()
        {
            this.CachedTransform = this.transform;
        }

        public Cell Initialize(Vector2Int position, CellType cellType, ICellEvent cellEvent, CellMapper cellMapper)
        {
            this.Position = position;
            this.Type = cellType;
            this.cellMapper = cellMapper;

            var constants = GameSystem.Instance.MasterData.Cell.Constants;
            this.CachedTransform.position = new Vector3(position.x * (constants.Scale.x + constants.Interval), 0.0f, position.y * (constants.Scale.z + constants.Interval));
            this.scalableObject.localScale = constants.Scale;
            this.boxCollider.center = new Vector3(0.0f, constants.Scale.y / 2.0f, 0.0f);
            this.boxCollider.size = constants.Scale;

            this.cellMapper.Add(this);
            this.AddEvent(cellEvent);

            return this;
        }

        public void AddEvent(ICellEvent cellEvent)
        {
            if(this.cellEvent != null)
            {
                this.Broker.Publish(ReleasedCellEvent.Get());
            }

            this.cellEvent = cellEvent;
            if(this.cellEvent != null)
            {
                this.cellMapper.RegisterHasEvent(this);
                this.cellEvent.OnRegister(this);
                this.CreateGimmickController();
            }
            else
            {
                this.cellMapper.RegisterNotHasEvent(this);
                this.DestroyGimmickController();
            }
        }

        public void ClearEvent()
        {
            this.AddEvent(null);
        }

        public void OnClickDown()
        {
        }

        public void OnClickUp()
        {
            if(this.cellEvent == null)
            {
                return;
            }

            this.cellEvent.OnClick(this);
        }

        public IObservable<ReleasedCellEvent> ReleasedCellEventAsObservable()
        {
            return this.Broker.Receive<ReleasedCellEvent>();
        }

        private void CreateGimmickController()
        {
            this.DestroyGimmickController();

            var constants = GameSystem.Instance.MasterData.Cell.Constants;
            this.gimmickController = this.cellEvent.CreateGimmickController();
            this.gimmickController.transform.SetParent(this.CachedTransform);
            this.gimmickController.transform.localPosition = new Vector3(0.0f, constants.Scale.y, 0.0f);
            // this.gimmickController.transform.localScale = constants.EffectScale;
        }

        private void DestroyGimmickController()
        {
            if(this.gimmickController == null)
            {
                return;
            }

            Destroy(this.gimmickController.gameObject);
            this.gimmickController = null;
        }
    }
}
