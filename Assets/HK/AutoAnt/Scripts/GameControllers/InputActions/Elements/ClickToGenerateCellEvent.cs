using HK.AutoAnt.CameraControllers;
using HK.AutoAnt.CellControllers;
using HK.AutoAnt.Extensions;
using HK.AutoAnt.InputControllers;
using HK.AutoAnt.Systems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// クリックイベントでセルにイベントを追加する
    /// </summary>
    public sealed class ClickToGenerateCellEvent : IInputAction<InputControllers.Events.ClickData>
    {
        private readonly CellEventGenerator eventGenerator;

        public ClickToGenerateCellEvent(CellEventGenerator eventGenerator)
        {
            this.eventGenerator = eventGenerator;
        }

        public void Do(InputControllers.Events.ClickData data)
        {
            var gameSystem = GameSystem.Instance;
            var cell = CellManager.GetCell(gameSystem.Cameraman.Camera.ScreenPointToRay(data.Position));
            if(cell == null)
            {
                return;
            }

            var evalute = this.eventGenerator.CanGenerate(cell, this.eventGenerator.RecordId);
            if(evalute == Constants.CellEventGenerateEvalute.Possible)
            {
                var levelUpCostRecord = gameSystem.MasterData.LevelUpCost.Records.Get(this.eventGenerator.RecordId, 0);
                Assert.IsNotNull(levelUpCostRecord);

                levelUpCostRecord.Cost.Consume(gameSystem.User, gameSystem.MasterData.Item);

                this.eventGenerator.Generate(cell, this.eventGenerator.RecordId, false);
            }
        }
    }
}
