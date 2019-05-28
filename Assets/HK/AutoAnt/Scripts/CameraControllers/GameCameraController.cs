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
        public void Move(float forwardVelocity, float rightVelocity)
        {
            var cameraman = GameSystem.Instance.Cameraman;
            cameraman.Position -= cameraman.ToFirstPersonVector(forwardVelocity, rightVelocity);
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
