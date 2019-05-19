using System;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.InputControllers.Modules
{
    /// <summary>
    /// 入力を制御するインターフェイス
    /// </summary>
    public interface IInputModule
    {
        IMessageBroker Broker { get; }

        IObservable<Events.Click> ClickAsObservable();

        IObservable<Events.ClickUp> ClickUpAsObservable();

        IObservable<Events.ClickDown> ClickDownAsObservable();
    }
}
