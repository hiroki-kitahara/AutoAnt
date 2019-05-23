using HK.AutoAnt.CameraControllers;
using HK.AutoAnt.CellControllers;
using HK.AutoAnt.InputControllers;
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
            var cell = CellManager.GetCell(Cameraman.Instance.Camera.ScreenPointToRay(data.Position));
            if(cell != null && !cell.HasEvent)
            {
                this.eventGenerator.Generate(cell);
            }
        }
    }
}
