﻿using HK.AutoAnt.CellControllers.ClickEvents;
using HK.AutoAnt.CellControllers.Gimmicks;
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

        private ICellEvent cellEvent = null;

        private CellMapper cellMapper;

        public Transform CachedTransform{ get; private set; }

        private CellGimmickController gimmickController;

        private CellSpec cellSpec;

        public bool HasEvent => this.cellEvent != null;

        void Awake()
        {
            this.CachedTransform = this.transform;
        }

        public Cell Initialize(Vector2Int id, CellType cellType, CellSpec cellSpec, ICellEvent clickEvent, CellMapper cellMapper)
        {
            this.Id = id;
            this.Type = cellType;
            this.cellEvent = clickEvent;
            this.cellMapper = cellMapper;
            this.cellSpec = cellSpec;

            this.CachedTransform.position = new Vector3(id.x * (cellSpec.Scale.x + cellSpec.Interval), 0.0f, id.y * (cellSpec.Scale.z + cellSpec.Interval));
            this.scalableObject.localScale = cellSpec.Scale;
            this.boxCollider.center = new Vector3(0.0f, cellSpec.Scale.y / 2.0f, 0.0f);
            this.boxCollider.size = cellSpec.Scale;

            return this;
        }

        public void AddEvent(ICellEvent cellEvent)
        {
            this.cellEvent = cellEvent;
            if(this.cellEvent != null)
            {
                this.cellMapper.RegisterHasEvent(this);
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

        private void CreateGimmickController()
        {
            this.DestroyGimmickController();

            this.gimmickController = this.cellEvent.CreateGimmickController();
            this.gimmickController.transform.SetParent(this.CachedTransform);
            this.gimmickController.transform.localPosition = new Vector3(0.0f, this.cellSpec.Scale.y, 0.0f);
            this.gimmickController.transform.localScale = this.cellSpec.EffectScale;
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