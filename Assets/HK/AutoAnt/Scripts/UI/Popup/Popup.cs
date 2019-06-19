using System;
using HK.AutoAnt.Events;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// ポップアップの抽象クラス
    /// </summary>
    public abstract class Popup : MonoBehaviour, IPopup
    {
        protected readonly MessageBroker broker = new MessageBroker();

        public IMessageBroker Broker => this.broker;

        /// <summary>
        /// 開く
        /// </summary>
        public virtual void Open()
        {
            this.gameObject.SetActive(true);

            this.Broker.Receive<PopupEvents.CompleteClose>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.broker.Dispose();
                })
                .AddTo(this);
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        public virtual void Close()
        {
            this.Broker.Publish(PopupEvents.StartClose.Get());

            this.gameObject.SetActive(false);

            this.Broker.Publish(PopupEvents.CompleteClose.Get());
        }
    }
}
