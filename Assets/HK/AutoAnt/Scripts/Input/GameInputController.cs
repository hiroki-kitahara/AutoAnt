using HK.AutoAnt.CameraControllers;
using HK.AutoAnt.CellControllers;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.InputControllers
{
    /// <summary>
    /// ゲームの入力を制御する
    /// </summary>
    public sealed class GameInputController : MonoBehaviour
    {
        [SerializeField]
        private Cameraman cameraman = null;

        [SerializeField]
        private CellManager cellManager = null;

        [SerializeField]
        private GameCameraController gameCameraController = null;

        void Awake()
        {
            var inputModule = InputControllers.Input.Current;

            inputModule.ClickDownAsObservable()
                .Where(x => x.Data.ButtonId == 0)
                .SubscribeWithState(this, (x, _this) =>
                {
                    var ray = _this.cameraman.Camera.ScreenPointToRay(x.Data.Position);
                    var clickableObject = _this.cellManager.GetClickableObject(ray);
                    if (clickableObject != null)
                    {
                        clickableObject.OnClickDown();
                    }
                })
                .AddTo(this);

            inputModule.ClickUpAsObservable()
                .Where(x => x.Data.ButtonId == 0)
                .SubscribeWithState(this, (x, _this) =>
                {
                    var ray = _this.cameraman.Camera.ScreenPointToRay(x.Data.Position);
                    var clickableObject = _this.cellManager.GetClickableObject(ray);
                    if (clickableObject != null)
                    {
                        clickableObject.OnClickUp();
                    }
                })
                .AddTo(this);

            inputModule.DragAsObservable()
                .SubscribeWithState(this, (x, _this) =>
                {
                    // FIXME: ドラッグ移動量をオプションか何かで編集出来るように
                    _this.gameCameraController.Move(x.Data.DeltaPosition.y * 0.05f, x.Data.DeltaPosition.x * 0.05f);
                })
                .AddTo(this);
        }
    }
}
