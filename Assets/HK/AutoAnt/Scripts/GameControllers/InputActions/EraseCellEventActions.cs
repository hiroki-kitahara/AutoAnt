using HK.AutoAnt.CameraControllers;
using HK.AutoAnt.CellControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// セルのイベントを削除するアクション
    /// </summary>
    public sealed class EraseCellEventActions : InputActions
    {
        public EraseCellEventActions(CellEventGenerator eventGenerator, CellMapper cellMapper, GameCameraController gameCameraController)
            : base(
                null,
                null,
                new ClickToEraseCellEvent(eventGenerator, cellMapper),
                new DragToMoveCamera(gameCameraController),
                null
            )
        {
        }
    }
}
