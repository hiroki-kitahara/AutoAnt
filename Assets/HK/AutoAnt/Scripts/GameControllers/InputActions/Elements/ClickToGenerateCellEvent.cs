using HK.AutoAnt.CameraControllers;
using HK.AutoAnt.CellControllers;
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
            var cell = CellManager.GetCell(GameSystem.Instance.Cameraman.Camera.ScreenPointToRay(data.Position));
            if(cell == null)
            {
                return;
            }

            if(this.eventGenerator.CanGenerate(cell, this.eventGenerator.RecordId))
            {
                this.eventGenerator.Generate(cell, this.eventGenerator.RecordId, false);
            }
        }
    }
}
