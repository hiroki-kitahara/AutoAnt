﻿using System;
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

        private CellMapper cellMapper;

        public Transform CachedTransform{ get; private set; }

        private CellGimmickController gimmickController;

        void Awake()
        {
            this.CachedTransform = this.transform;
        }

        public Cell Initialize(Vector2Int position, CellType cellType, CellMapper cellMapper)
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

            return this;
        }

        public bool HasEvent => this.cellMapper.HasEvent(this);

        public void ClearEvent()
        {
            Assert.IsTrue(this.HasEvent);

            this.cellMapper.Remove(this.cellMapper.EventMap[this.Position]);
        }

        public void OnClickDown()
        {
        }

        public void OnClickUp()
        {
            if(!this.HasEvent)
            {
                return;
            }

            this.cellMapper.EventMap[this.Position].OnClick(this);
        }

        public IObservable<ReleasedCellEvent> ReleasedCellEventAsObservable()
        {
            return this.Broker.Receive<ReleasedCellEvent>();
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
