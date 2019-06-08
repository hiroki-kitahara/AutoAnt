using System;
using HK.AutoAnt.CellControllers.Gimmicks;
using HK.AutoAnt.Systems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using HK.AutoAnt.Extensions;

namespace HK.AutoAnt.CellControllers.Events
{
    /// <summary>
    /// <see cref="Cell"/>のイベントを持つ抽象クラス
    /// </summary>
    public abstract class CellEvent : ScriptableObject, ICellEvent
    {
        [SerializeField]
        private CellEventGenerateCondition condition = null;

        [SerializeField]
        protected int size = 1;
        public int Size => this.size;

        [SerializeField]
        private AudioClip buildingSE;
        public AudioClip BuildingSE => this.buildingSE;

        [SerializeField]
        private AudioClip destructionSE;
        public AudioClip DestructionSE => this.destructionSE;

        public Vector2Int Origin { get; protected set; }

        /// <summary>
        /// 実体が持つイベント
        /// </summary>
        protected readonly CompositeDisposable instanceEvents = new CompositeDisposable();

        protected CellGimmickController gimmick;

        public abstract CellGimmickController CreateGimmickController();

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            this.size = this.size <= 1 ? 1 : this.size;
        }
#endif

        public virtual void Initialize(Vector2Int position, GameSystem gameSystem)
        {
            this.Origin = position;
            this.gimmick = this.CreateGimmickController();
            AutoAntSystem.Audio.SE.Play(this.buildingSE);
        }

        public virtual void Remove(GameSystem gameSystem)
        {
            this.instanceEvents.Clear();
            Destroy(this.gimmick.gameObject);
            AutoAntSystem.Audio.SE.Play(this.destructionSE);
        }

        public bool CanGenerate(Cell origin, int cellEventRecordId, GameSystem gameSystem, CellMapper cellMapper)
        {
            Assert.IsNotNull(this.condition);

            var cellPositions = cellMapper.GetRange(origin.Position, Vector2Int.one * this.size, p => cellMapper.Cell.Map.ContainsKey(p));

            // 配置したいところにセルがない場合は生成できない
            if(cellPositions.Length != this.size * this.size)
            {
                return false;
            }

            // 配置したいセルにイベントがあった場合は生成できない
            var cells = cellMapper.GetCells(cellPositions);
            if(Array.FindIndex(cells, c => cellMapper.HasEvent(c)) != -1)
            {
                return false;
            }

            // コストが満たしていない場合は生成できない
            var masterData = gameSystem.MasterData.LevelUpCost.Records.Get(cellEventRecordId, 0);
            if(!masterData.Cost.IsEnough(gameSystem.User, gameSystem.MasterData.Item))
            {
                return false;
            }

            return this.condition.Evalute(cells);
        }

        public virtual void OnClick(Cell owner)
        {
        }
    }
}
