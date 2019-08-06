using HK.AutoAnt.CameraControllers;
using HK.AutoAnt.CellControllers;
using HK.AutoAnt.InputControllers;
using HK.AutoAnt.Systems;
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

        private readonly CellMapper cellMapper;

        public ClickToEraseCellEvent(CellEventGenerator eventGenerator, CellMapper cellMapper)
        {
            this.eventGenerator = eventGenerator;
            this.cellMapper = cellMapper;
        }

        public void Do(InputControllers.Events.ClickData data)
        {
            var cell = CellManager.GetCell(GameSystem.Instance.Cameraman.Camera.ScreenPointToRay(data.Position));
            if(cell != null && this.cellMapper.HasEvent(cell))
            {
                this.eventGenerator.Remove(cell);
            }
        }
    }
}
