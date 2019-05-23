using HK.AutoAnt.CameraControllers;
using HK.AutoAnt.InputControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// ドラッグ操作によるカメラ移動処理を行うアクション
    /// </summary>
    public sealed class DragToMoveCamera : IInputAction<InputControllers.Events.DragData>
    {
        private readonly GameCameraController gameCameraController;

        public DragToMoveCamera(GameCameraController gameCameraController)
        {
            this.gameCameraController = gameCameraController;
        }
        
        public void Do(InputControllers.Events.DragData data)
        {
            // FIXME: ドラッグ移動量をオプションか何かで編集出来るように
            var delta = data.DeltaPosition * 0.05f;
            this.gameCameraController.Move(delta.y, delta.x);
        }
    }
}
