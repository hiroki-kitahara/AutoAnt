using HK.AutoAnt.CameraControllers;
using HK.AutoAnt.InputControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// スクロール操作によるカメラズームイン/アウト処理を行うアクション
    /// </summary>
    public sealed class ScrollToZoomCamera : IInputAction<InputControllers.Events.ScrollData>
    {
        private readonly GameCameraController gameCameraController;

        public ScrollToZoomCamera(GameCameraController gameCameraController)
        {
            this.gameCameraController = gameCameraController;
        }

        public void Do(InputControllers.Events.ScrollData data)
        {
            // FIXME: ズーム移動量をオプションか何かで編集出来るように
            var delta = data.DirectionId * 0.25f;
            this.gameCameraController.Zoom(delta);
        }
    }
}
