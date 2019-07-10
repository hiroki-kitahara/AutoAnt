using HK.AutoAnt.Events;
using HK.AutoAnt.Systems;
using HK.AutoAnt.UI;
using HK.Framework.EventSystems;
using UniRx;
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
        private float offsetCellEventDetailsPopupFocus = 0.0f;

        void Awake()
        {
            Broker.Global.Receive<PopupEvents.StartOpen>()
                .Select(x => x.Popup as CellEventDetailsPopup)
                .Where(x => x != null)
                .SubscribeWithState(this, (x, _this) =>
                {
                    var position = x.SelectCellEvent.Gimmick.transform.position;
                    var cameraman = GameSystem.Instance.Cameraman;
                    var camera = cameraman.Camera;
                    var forward = Vector3.Scale(camera.transform.forward, new Vector3(1.0f, 0.0f, 1.0f)).normalized;
                    var offset = forward * _this.offsetCellEventDetailsPopupFocus;

                    cameraman.Position = new Vector3(position.x, 0.0f, position.z) + offset;
                })
                .AddTo(this);
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
