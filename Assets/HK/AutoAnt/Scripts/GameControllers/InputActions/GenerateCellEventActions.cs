using HK.AutoAnt.CameraControllers;
using HK.AutoAnt.CellControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// セルにイベントを追加するアクション
    /// </summary>
    public sealed class GenerateCellEventActions : InputActions
    {
        public GenerateCellEventActions(CellEventGenerator eventGenerator, GameCameraController gameCameraController)
            : base(
                null,
                null,
                new ClickToGenerateCellEvent(eventGenerator),
                new DragToMoveCamera(gameCameraController)
            )
        {
        }
    }
}
