using HK.AutoAnt.Systems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CameraControllers
{
    /// <summary>
    /// ゲーム用にカメラを制御する
    /// </summary>
    public sealed class GameCameraController : MonoBehaviour
    {
        public void Move(Vector2 deltaPosition)
        {
            var cameraman = GameSystem.Instance.Cameraman;
            var camera = cameraman.Camera;
            var size = camera.orthographicSize;
            var ratioX = size * 2.0f / Screen.height;
            var ratioY = size * 2.0f / Screen.width * camera.aspect;
            cameraman.Position -= cameraman.ToFirstPersonVector(deltaPosition.y * ratioY, deltaPosition.x * ratioX);
        }

        public void Zoom(float velocity)
        {
            var cameraman = GameSystem.Instance.Cameraman;
            // ズームの限界以上は動かさない
            if(cameraman.Size <= velocity && velocity > 0f)
            {
                return;
            }
            cameraman.Size -= velocity;
        }
    }
}
