using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// 入力に対する様々なアクションを行う抽象クラス
    /// </summary>
    public abstract class InputActions
    {
        public readonly IInputAction<InputControllers.Events.ClickData> ClickAction;

        public readonly IInputAction<InputControllers.Events.ClickData> ClickDownAction;

        public readonly IInputAction<InputControllers.Events.ClickData> ClickUpAction;

        public readonly IInputAction<InputControllers.Events.DragData> DragAction;

        public readonly IInputAction<InputControllers.Events.ScrollData> ScrollAction;

        public InputActions(
            IInputAction<InputControllers.Events.ClickData> clickAction,
            IInputAction<InputControllers.Events.ClickData> clickDownAction,
            IInputAction<InputControllers.Events.ClickData> clickUpAction,
            IInputAction<InputControllers.Events.DragData> dragAction,
            IInputAction<InputControllers.Events.ScrollData> scrollAction
        )
        {
            this.ClickAction = clickAction;
            this.ClickDownAction = clickDownAction;
            this.ClickUpAction = clickUpAction;
            this.DragAction = dragAction;
            this.ScrollAction = scrollAction;
        }
    }
}
