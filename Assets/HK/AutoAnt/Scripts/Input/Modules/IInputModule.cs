using System;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace HK.AutoAnt.InputControllers.Modules
{
    /// <summary>
    /// 入力を制御するインターフェイス
    /// </summary>
    public interface IInputModule
    {
        IMessageBroker Broker { get; }

        int MainPointerId { get; }

        IObservable<Events.Click> ClickAsObservable();

        IObservable<Events.ClickUp> ClickUpAsObservable();

        IObservable<Events.ClickDown> ClickDownAsObservable();

        IObservable<Events.Drag> DragAsObservable();

        IObservable<Events.Scroll> ScrollAsObservable();

        void OnDrag(PointerEventData eventData);

        void OnPointerClick(PointerEventData eventData);
        
        void OnPointerDown(PointerEventData eventData);
    }
}
