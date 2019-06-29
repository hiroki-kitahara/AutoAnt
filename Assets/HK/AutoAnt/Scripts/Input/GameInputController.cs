using HK.AutoAnt.CameraControllers;
using HK.AutoAnt.CellControllers;
using HK.AutoAnt.Events;
using HK.AutoAnt.GameControllers;
using HK.AutoAnt.Systems;
using HK.Framework.EventSystems;
using HK.AutoAnt.Constants;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using System;
using System.Collections.Generic;


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

        private InputMode currentMode = InputMode.ClickMode;

        private Dictionary<InputMode, InputMode> modeRotation = new Dictionary<InputMode, InputMode>();
        private Dictionary<InputMode, Action> modeActions = new Dictionary<InputMode, Action>();

        private ClickToClickableObjectActions cachedClickToClickableObjectActions;

        private GenerateCellEventActions cachedGenerateCellEventActions;

        private EraseCellEventActions cachedEraseCellEventActions;

        private DevelopCellActions cachedDevelopCellActions;

        void Awake()
        {
            this.cachedClickToClickableObjectActions = new ClickToClickableObjectActions(this.gameCameraController);
            this.cachedGenerateCellEventActions = new GenerateCellEventActions(this.cellManager.EventGenerator, this.gameCameraController);
            this.cachedEraseCellEventActions = new EraseCellEventActions(this.cellManager.EventGenerator, this.cellManager.Mapper, this.gameCameraController);
            this.cachedDevelopCellActions = new DevelopCellActions(
                GameSystem.Instance,
                this.cellManager.CellGenerator,
                this.cellManager.Mapper,
                100100,
                100000,
                1,
                this.gameCameraController
                );

            ModeRotationInitialize();
            ModeActionInitialize();

            Broker.Global.Receive<RequestChangeInputMode>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    currentMode = modeRotation[currentMode];
                    modeActions[currentMode]();
                    Broker.Global.Publish(ChangedInputMode.Get(currentMode));
                })
                .AddTo(this);

            Broker.Global.Receive<RequestBuildingMode>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.inputActions = _this.cachedGenerateCellEventActions;
                })
                .AddTo(this);

            var inputModule = InputControllers.Input.Current;
            this.inputActions = new ClickToClickableObjectActions(this.gameCameraController);

            inputModule.ClickDownAsObservable()
                .Where(x => x.Data.ButtonId == inputModule.MainPointerId)
                .Where(_ => this.inputActions.ClickDownAction != null)
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.inputActions.ClickDownAction.Do(x.Data);
                })
                .AddTo(this);

            inputModule.ClickUpAsObservable()
                .Where(x => x.Data.ButtonId == inputModule.MainPointerId)
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
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha1))
            {
                GameSystem.Instance.CellManager.EventGenerator.RecordId = 100000;
                Broker.Global.Publish(RequestNotification.Get($"建設するID = {GameSystem.Instance.CellManager.EventGenerator.RecordId}"));
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha2))
            {
                GameSystem.Instance.CellManager.EventGenerator.RecordId = 101000;
                Broker.Global.Publish(RequestNotification.Get($"建設するID = {GameSystem.Instance.CellManager.EventGenerator.RecordId}"));
            }
            
            /*
            if (UnityEngine.Input.GetKeyDown(KeyCode.Q))
            {
                this.inputActions = new ClickToClickableObjectActions(this.gameCameraController);
                Broker.Global.Publish(RequestNotification.Get("クリックモード"));
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.W))
            {
                this.inputActions = new GenerateCellEventActions(this.cellManager.EventGenerator, this.gameCameraController);
                Broker.Global.Publish(RequestNotification.Get("建設モード"));
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.E))
            {
                this.inputActions = new EraseCellEventActions(this.cellManager.EventGenerator, this.cellManager.Mapper, this.gameCameraController);
                Broker.Global.Publish(RequestNotification.Get("解体モード"));
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.R))
            {
                this.inputActions = new DevelopCellActions(
                    GameSystem.Instance,
                    this.cellManager.CellGenerator,
                    this.cellManager.Mapper,
                    100100,
                    100000,
                    1,
                    this.gameCameraController
                    );

                Broker.Global.Publish(RequestNotification.Get("開拓モード"));
            }
            */
        }

        private void ModeRotationInitialize()
        {
            // モード切替用テーブル
            modeRotation[InputMode.ClickMode] = InputMode.BuildMode;
            modeRotation[InputMode.BuildMode] = InputMode.DismantleMode;
            modeRotation[InputMode.DismantleMode] = InputMode.ExploringMode;
            modeRotation[InputMode.ExploringMode] = InputMode.ClickMode;
        }

        private void ModeActionInitialize()
        {

            // クリックモード時の挙動
            modeActions[InputMode.ClickMode] = () =>
            {
                this.inputActions = this.cachedClickToClickableObjectActions;
            };

            // 建設モード時の挙動
            modeActions[InputMode.BuildMode] = () =>
            {
                this.inputActions = this.cachedGenerateCellEventActions;
            };

            // 解体モード時の挙動
            modeActions[InputMode.DismantleMode] = () =>
            {
                this.inputActions = this.cachedEraseCellEventActions;
            };

            // 開拓モード時の挙動
            modeActions[InputMode.ExploringMode] = () =>
            {
                this.inputActions = this.cachedDevelopCellActions;
            };
        }
    }
}
