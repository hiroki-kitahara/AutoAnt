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

        private readonly GameSystem gameSystem;

        private readonly CellMapper cellMapper;

        private CellEvent GeneratableCellEvent => GameSystem.Instance.MasterData.CellEvent.Records.Get(this.RecordId).EventData;

        public CellEventGenerator(GameSystem gameSystem, CellMapper cellMapper)
        {
            this.gameSystem = gameSystem;
            this.cellMapper = cellMapper;
        }

        public void Generate(Cell cell, int cellEventRecordId, bool isInitializingGame)
        {
            Assert.IsFalse(this.cellMapper.HasEvent(cell));

            var cellEventRecord = this.gameSystem.MasterData.CellEvent.Records.Get(cellEventRecordId);
            Assert.IsNotNull(cellEventRecord);
            Assert.IsNotNull(cellEventRecord.EventData);

            var levelUpCostRecord = this.gameSystem.MasterData.LevelUpCost.Records.Get(cellEventRecordId, 0);
            Assert.IsNotNull(levelUpCostRecord);

            levelUpCostRecord.Cost.Consume(this.gameSystem.User, this.gameSystem.MasterData.Item);

            var cellEventInstance = UnityEngine.Object.Instantiate(cellEventRecord.EventData);

            // (Clone)という文字列が要らないのでnameを代入する必要がある
            cellEventInstance.name = cellEventRecord.EventData.name;
            cellEventInstance.Initialize(cell.Position, this.gameSystem, isInitializingGame);
            cellMapper.Add(cellEventInstance);

            this.gameSystem.User.History.GenerateCellEvent.Add(cellEventRecordId, 0);

            Broker.Global.Publish(AddedCellEvent.Get(cellEventInstance));
        }

        public void GenerateOnDeserialize(CellEvent instance)
        {
            instance.Initialize(instance.Origin, this.gameSystem, true);
            this.cellMapper.Add(instance);

            Broker.Global.Publish(AddedCellEvent.Get(instance));
        }

        public void Erase(Cell cell)
        {
            Assert.IsTrue(this.cellMapper.HasEvent(cell));
            var cellEvent = this.cellMapper.CellEvent.Map[cell.Position];
            this.cellMapper.Remove(cellEvent);
            cellEvent.Remove(this.gameSystem);

            Broker.Global.Publish(RemovedCellEvent.Get(cellEvent));
        }

        /// <summary>
        /// イベントが作成可能か返す
        /// </summary>
        public bool CanGenerate(Cell cell, int cellEventRecordId)
        {
            return this.GeneratableCellEvent.CanGenerate(cell, cellEventRecordId, this.gameSystem, this.cellMapper);
        }
    }
}
