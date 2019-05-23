using HK.AutoAnt.CameraControllers;
using HK.AutoAnt.CellControllers;
using HK.AutoAnt.InputControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// クリックイベントでセルのイベントを削除するアクション
    /// </summary>
    public sealed class ClickToEraseCellEvent : IInputAction<InputControllers.Events.ClickData>
    {
        private readonly CellEventGenerator eventGenerator;

        public ClickToEraseCellEvent(CellEventGenerator eventGenerator)
        {
            this.eventGenerator = eventGenerator;
        }

        public void Do(InputControllers.Events.ClickData data)
        {
            var cell = CellManager.GetCell(Cameraman.Instance.Camera.ScreenPointToRay(data.Position));
            if(cell != null && cell.HasEvent)
            {
                this.eventGenerator.Erase(cell);
            }
        }
    }
}
