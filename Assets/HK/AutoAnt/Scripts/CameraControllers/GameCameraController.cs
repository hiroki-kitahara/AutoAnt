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

            // ズームの限界以上は動かさない
            if(Cameraman.Instance.Size <= velocity && velocity > 0f)
            {
                return;
            }
            Cameraman.Instance.Size -= velocity;
        }
    }
}
