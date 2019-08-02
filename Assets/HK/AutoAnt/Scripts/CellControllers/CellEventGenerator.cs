using System;
using HK.AutoAnt.CellControllers.Events;
using HK.AutoAnt.Events;
using HK.AutoAnt.Extensions;
using HK.AutoAnt.GameControllers;
using HK.AutoAnt.Systems;
using HK.Framework.EventSystems;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers
{
    /// <summary>
    /// <see cref="Cell"/>のイベントを生成する
    /// </summary>
    public sealed class CellEventGenerator : IMasterDataCellEventRecordIdHolder
    {
        /// <summary>
        /// 作成可能なセルイベントのレコードID
        /// </summary>
        public int RecordId { get; set; } = 100000;

        private readonly CellMapper cellMapper;

        private CellEvent GeneratableCellEvent => GameSystem.Instance.MasterData.CellEvent.Records.Get(this.RecordId).EventData;

        public CellEventGenerator(CellMapper cellMapper)
        {
            this.cellMapper = cellMapper;

            Broker.Global.Receive<RequestBuildingMode>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.RecordId = x.BuildingCellEventRecord.Id;
                })
                .AddTo(GameSystem.Instance);
        }

        public void Generate(Cell cell, int cellEventRecordId, bool isInitializingGame)
        {
            Assert.IsFalse(this.cellMapper.HasEvent(cell));

            var gameSystem = GameSystem.Instance;
            var cellEventRecord = gameSystem.MasterData.CellEvent.Records.Get(cellEventRecordId);
            Assert.IsNotNull(cellEventRecord);
            Assert.IsNotNull(cellEventRecord.EventData);

            var cellEventInstance = UnityEngine.Object.Instantiate(cellEventRecord.EventData);

            // (Clone)という文字列が要らないのでnameを代入する必要がある
            cellEventInstance.name = cellEventRecord.EventData.name;
            cellMapper.Add(cellEventInstance, cell.Position);
            cellEventInstance.Initialize(cell.Position, isInitializingGame);

            gameSystem.User.History.GenerateCellEvent.Add(cellEventRecordId, 0);

            Broker.Global.Publish(AddedCellEvent.Get(cellEventInstance));
        }

        public void GenerateOnDeserialize(CellEvent instance)
        {
            this.cellMapper.Add(instance, instance.Origin);
            instance.Initialize(instance.Origin, true);

            Broker.Global.Publish(AddedCellEvent.Get(instance));
        }

        public void Remove(Cell cell)
        {
            Assert.IsTrue(this.cellMapper.HasEvent(cell));
            var cellEvent = this.cellMapper.CellEvent.Map[cell.Position];
            this.Remove(cellEvent);
        }

        public void Remove(ICellEvent cellEvent)
        {
            this.cellMapper.Remove(cellEvent);
            cellEvent.Remove();

            Broker.Global.Publish(RemovedCellEvent.Get(cellEvent));
        }

        /// <summary>
        /// イベントが作成可能か返す
        /// </summary>
        public Constants.CellEventGenerateEvalute CanGenerate(Cell cell, int cellEventRecordId)
        {
            return this.GeneratableCellEvent.CanGenerate(cell, cellEventRecordId, GameSystem.Instance, this.cellMapper);
        }
    }
}
