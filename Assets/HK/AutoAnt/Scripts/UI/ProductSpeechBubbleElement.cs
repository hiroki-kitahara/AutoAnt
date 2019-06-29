using HK.AutoAnt.CellControllers.Events;
using HK.AutoAnt.Events;
using HK.AutoAnt.Systems;
using HK.Framework.EventSystems;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ProductSpeechBubbleElement : MonoBehaviour
    {
        private Facility facility;

        private Canvas canvas;

        private RectTransform canvasTransform;

        private RectTransform rectTransform;

        public void Initialize(Facility facility, ProductSpeechBubbleController owner, Canvas canvas)
        {
            Assert.IsNotNull(facility);
            Assert.IsNotNull(canvas);

            this.facility = facility;
            this.canvas = canvas;
            this.canvasTransform = canvas.transform as RectTransform;
            this.rectTransform = this.transform as RectTransform;

            owner.LateUpdateAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    var worldCamera = GameSystem.Instance.Cameraman.Camera;
                    var uiCamera = _this.canvas.worldCamera;
                    var pos = Vector2.zero;
                    var screenPos = RectTransformUtility.WorldToScreenPoint(worldCamera, _this.facility.Gimmick.transform.position);
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(_this.canvasTransform, screenPos, uiCamera, out pos);

                    _this.rectTransform.localPosition = pos;
                })
                .AddTo(this);

            Broker.Global.Receive<RemovedCellEvent>()
                .Where(x => x.CellEvent.Equals(this.facility))
                .SubscribeWithState(this, (_, _this) =>
                {
                    Destroy(_this.gameObject);
                })
                .AddTo(this);
        }
    }
}
