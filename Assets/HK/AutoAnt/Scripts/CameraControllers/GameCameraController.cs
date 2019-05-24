using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CameraControllers
{
    /// <summary>
    /// ゲーム用にカメラを制御する
    /// </summary>
    public sealed class GameCameraController : MonoBehaviour
    {
        [SerializeField]
        private Cameraman cameraman = null;

        public void Move(float forwardVelocity, float rightVelocity)
        {
            Cameraman.Instance.Position -= this.cameraman.ToFirstPersonVector(forwardVelocity, rightVelocity);
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
