using HK.AutoAnt.CameraControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// <see cref="IClickableObject"/>をクリックするアクション
    /// </summary>
    public sealed class ClickToClickableObjectActions : InputActions
    {
        public ClickToClickableObjectActions(GameCameraController gameCameraController)
            : base(
                null,
                new ClickDownToClickableObject(),
                new ClickUpToClickableObject(),
                new DragToMoveCamera(gameCameraController),
                new ScrollToZoomCamera(gameCameraController)
            )
        {
        }
    }
}
