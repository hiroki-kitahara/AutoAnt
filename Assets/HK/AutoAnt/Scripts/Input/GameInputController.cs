using HK.AutoAnt.CameraControllers;
using HK.AutoAnt.CellControllers;
using HK.AutoAnt.GameControllers;
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
        private CellManager cellManager = null;

        [SerializeField]
        private GameCameraController gameCameraController = null;

        private InputActions inputActions;

        void Awake()
        {
            var inputModule = InputControllers.Input.Current;
            this.inputActions = new ClickToClickableObjectActions(this.gameCameraController);

            inputModule.ClickDownAsObservable()
                .Where(x => x.Data.ButtonId == 0)
                .Where(_ => this.inputActions.ClickDownAction != null)
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.inputActions.ClickDownAction.Do(x.Data);
                })
                .AddTo(this);

            inputModule.ClickUpAsObservable()
                .Where(x => x.Data.ButtonId == 0)
                .Where(_ => this.inputActions.ClickUpAction != null)
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.inputActions.ClickUpAction.Do(x.Data);
                })
                .AddTo(this);

            inputModule.DragAsObservable()
                .Where(_ => this.inputActions.DragAction != null)
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.inputActions.DragAction.Do(x.Data);
                })
                .AddTo(this);

            inputModule.ScrollAsObservable()
                .Where(_ => this.inputActions.ScrollAction != null)
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.inputActions.ScrollAction.Do(x.Data);
                })
                .AddTo(this);
        }

        void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
            {
                this.inputActions = new ClickToClickableObjectActions(this.gameCameraController);
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.W))
            {
                this.inputActions = new GenerateCellEventActions(this.cellManager.EventGenerator, this.gameCameraController);
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.E))
            {
                this.inputActions = new EraseCellEventActions(this.cellManager.EventGenerator, this.cellManager.Mapper, this.gameCameraController);
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.R))
            {
                this.inputActions = new DevelopCellActions(
                    this.cellManager.Generator,
                    this.cellManager.Mapper,
                    100100,
                    100000,
                    1,
                    this.gameCameraController
                    );
            }
        }
    }
}
