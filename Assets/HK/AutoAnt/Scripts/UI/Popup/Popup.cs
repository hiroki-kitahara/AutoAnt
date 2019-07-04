using System;
using HK.AutoAnt.Events;
using HK.Framework.EventSystems;
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
        /// <summary>
        /// 開く
        /// </summary>
        public virtual void Open()
        {
            Broker.Global.Publish(PopupEvents.StartOpen.Get(this));

            this.gameObject.SetActive(true);

            Broker.Global.Publish(PopupEvents.CompleteOpen.Get(this));
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        public virtual void Close()
        {
            Broker.Global.Publish(PopupEvents.StartClose.Get(this));

            this.gameObject.SetActive(false);

            Broker.Global.Publish(PopupEvents.CompleteClose.Get(this));
        }
    }
}
