using System;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.InputControllers.Modules
{
    /// <summary>
    /// スタンドアローンの入力制御クラス
    /// </summary>
    public sealed class Standalone : IInputModule
    {
        public IMessageBroker Broker => HK.Framework.EventSystems.Broker.Global;

        public IObservable<Events.Click> ClickAsObservable()
        {
            return Broker.Receive<Events.Click>();
        }

        public IObservable<Events.ClickDown> ClickDownAsObservable()
        {
            return Broker.Receive<Events.ClickDown>();
        }

        public IObservable<Events.ClickUp> ClickUpAsObservable()
        {
            return Broker.Receive<Events.ClickUp>();
        }

        public bool GetClick(int button)
        {
            return UnityEngine.Input.GetMouseButton(button);
        }

        public bool GetClickDown(int button)
        {
            return UnityEngine.Input.GetMouseButtonDown(button);
        }

        public bool GetClickUp(int button)
        {
            return UnityEngine.Input.GetMouseButtonUp(button);
        }
    }
}
