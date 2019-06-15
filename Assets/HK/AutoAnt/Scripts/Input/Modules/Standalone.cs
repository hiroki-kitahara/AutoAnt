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

        public int MainPointerId => -1;

        public Standalone()
        {
            Observable.EveryUpdate()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.UpdateScroll();
                });
        }

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

        public IObservable<Events.Drag> DragAsObservable()
        {
            return Broker.Receive<Events.Drag>();
        }

        public IObservable<Events.Scroll> ScrollAsObservable()
        {
            return Broker.Receive<Events.Scroll>();
        }

        private void UpdateScroll()
        {
            var amount = UnityEngine.Input.GetAxis("Mouse ScrollWheel");
            if (amount != 0f)
            {
                Input.Current.Broker.Publish(Events.Scroll.Get(Events.ScrollData.Get(amount)));
            }
        }
    }
}
