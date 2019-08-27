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

        [SerializeField]
        private float zoomMin = 1.0f;

        [SerializeField]
        private float zoomMax = 5.0f;

        void Awake()
        {
            // CellEventDetailsPopupが開いた時に指定されたセルイベントにカメラをフォーカスさせる
            Broker.Global.Receive<PopupEvents.StartOpen>()
                .Select(x => x.Popup as CellEventDetailsPopup)
                .Where(x => x != null)
                .SubscribeWithState(this, (x, _this) =>
                {
                    Assert.IsNotNull(x.SelectCellEvent);

                    var position = x.SelectCellEvent.Gimmick.transform.position;
                    var cameraman = GameSystem.Instance.Cameraman;
                    var camera = cameraman.Camera;
                    var forward = Vector3.Scale(camera.transform.forward, new Vector3(1.0f, 0.0f, 1.0f)).normalized;
                    var offset = forward * _this.offsetCellEventDetailsPopupFocus;

                    cameraman.Position = new Vector3(position.x, 0.0f, position.z) + offset;
                })
                .AddTo(this);

            Broker.Global.Receive<RequestCameraZoom>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.Zoom(x.Value);
                })
                .AddTo(this);
        }

        /// <summary>
        /// カメラを移動する
        /// </summary>
        public void Move(Vector2 deltaPosition)
        {
            var cameraman = GameSystem.Instance.Cameraman;
            var camera = cameraman.Camera;
            var size = camera.orthographicSize;
            var ratioX = size * 2.0f / Screen.height;
            var ratioY = size * 2.0f / Screen.width * camera.aspect;
            cameraman.Position -= cameraman.ToFirstPersonVector(deltaPosition.y * ratioY, deltaPosition.x * ratioX);
        }

        /// <summary>
        /// ズーム値を設定する
        /// </summary>
        public void Zoom(float value)
        {
            value = Mathf.Clamp01(value);
            value = 1.0f - value;
            var cameraman = GameSystem.Instance.Cameraman;
            cameraman.Size = ((this.zoomMax - this.zoomMin) * value) + this.zoomMin;
        }

        /// <summary>
        /// ズーム値を加算する
        /// </summary>
        public void ZoomOnVelocity(float velocity)
        {
            var currentSize = GameSystem.Instance.Cameraman.Size;
            var normalizedSize = (currentSize - this.zoomMin) / (this.zoomMax - this.zoomMin);
            var result = 1.0f - (normalizedSize - velocity);
            this.Zoom(result);
        }
    }
}
