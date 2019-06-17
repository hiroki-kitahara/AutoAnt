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
        void Update()
        {
            float vector = 0.1f;
            if (Input.GetKey(KeyCode.W))
            {
                this.Move(new Vector2(0.0f, vector));
            }
            if (Input.GetKey(KeyCode.S))
            {
                this.Move(new Vector2(0.0f, -vector));
            }
            if (Input.GetKey(KeyCode.A))
            {
                this.Move(new Vector2(-vector, 0.0f));
            }
            if (Input.GetKey(KeyCode.D))
            {
                this.Move(new Vector2(vector, 0.0f));
            }
        }
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
