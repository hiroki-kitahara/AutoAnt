using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.InputControllers.Updaters
{
    /// <summary>
    /// プラットフォームの入力更新処理を行うクラス
    /// </summary>
    public sealed class Standalone : IInputUpdater
    {
        private const int ButtonMax = 2;

        private const int MainButtonId = 0;

        private bool isDrag = false;

        private Vector3 lastClickPosition;

        private Vector3 currentPosition;

        private readonly InputSpec inputSpec;

        public Standalone(InputSpec inputSpec)
        {
            this.inputSpec = inputSpec;
        }

        public void Update()
        {
            this.UpdateClickEvents();
            this.UpdateDragEvents();
        }

        private void UpdateClickEvents()
        {
            for (var i = 0; i < ButtonMax; i++)
            {
                if (UnityEngine.Input.GetMouseButton(i))
                {
                    Input.Current.Broker.Publish(Events.Click.Get(i));
                }
                if (UnityEngine.Input.GetMouseButtonUp(i))
                {
                    Input.Current.Broker.Publish(Events.ClickUp.Get(i));
                }
                if (UnityEngine.Input.GetMouseButtonDown(i))
                {
                    Input.Current.Broker.Publish(Events.ClickDown.Get(i));
                }
            }
        }

        private void UpdateDragEvents()
        {
            if(UnityEngine.Input.GetMouseButtonDown(MainButtonId))
            {
                this.lastClickPosition = UnityEngine.Input.mousePosition;
                this.currentPosition = this.lastClickPosition;
            }

            if(UnityEngine.Input.GetMouseButton(MainButtonId))
            {
                var newPosition = UnityEngine.Input.mousePosition;
                if((this.lastClickPosition - newPosition).magnitude > this.inputSpec.DragThrehold)
                {
                    this.isDrag = true;
                    var velocity = newPosition - this.currentPosition;
                    Input.Current.Broker.Publish(Events.Drag.Get(velocity));
                    this.currentPosition = newPosition;
                }
            }
        }
    }
}
