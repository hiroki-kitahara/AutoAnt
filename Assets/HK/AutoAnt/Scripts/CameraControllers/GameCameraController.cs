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
        private Cameraman cameraman;

        public void Move(float forwardVelocity, float rightVelocity)
        {
            Cameraman.Instance.Root.position -= this.cameraman.ToFirstPersonVector(forwardVelocity, rightVelocity);
        }
    }
}
