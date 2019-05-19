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

        public void Update()
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
    }
}
