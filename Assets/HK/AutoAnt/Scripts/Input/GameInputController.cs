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
        private CellManager cellManager;

        void Awake()
        {
            var inputModule = InputControllers.Input.Current;

            inputModule.ClickDownAsObservable()
                .Where(x => x.ButtonId == 0)
                .SubscribeWithState(this, (x, _this) =>
                {
                    var clickableObject = this.cellManager.GetClickableObject();
                    if (clickableObject != null)
                    {
                        clickableObject.OnClickDown();
                    }
                })
                .AddTo(this);

            inputModule.ClickUpAsObservable()
                .Where(x => x.ButtonId == 0)
                .SubscribeWithState(this, (x, _this) =>
                {
                    var clickableObject = this.cellManager.GetClickableObject();
                    if (clickableObject != null)
                    {
                        clickableObject.OnClickUp();
                    }
                })
                .AddTo(this);

            inputModule.DragAsObservable()
                .SubscribeWithState(this, (x, _this) =>
                {
                    var cameraman = Cameraman.Instance;
                    var forward = Vector3.Scale(cameraman.Camera.transform.forward, new Vector3(1.0f, 0.0f, 1.0f)).normalized;
                    var right = cameraman.Camera.transform.right;
                    Cameraman.Instance.Root.position -= ((forward * x.DeltaPosition.y) + (right * x.DeltaPosition.x)) * 0.05f;
                })
                .AddTo(this);
        }
    }
}
