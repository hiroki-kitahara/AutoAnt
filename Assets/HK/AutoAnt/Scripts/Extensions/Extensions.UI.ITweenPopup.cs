using System;
using System.Collections.Generic;
using System.Linq;
using HK.AutoAnt.Events;
using HK.AutoAnt.UI;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;

namespace HK.AutoAnt.Extensions
{
    /// <summary>
    /// <see cref="ITweenPopup"/>に関する拡張関数
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// ツイーンしながら表示を開始する
        /// </summary>
        public static void StartTweeningOpen(this ITweenPopup self)
        {
            Broker.Global.Publish(PopupEvents.StartOpen.Get(self));

            var maxDuration = float.MinValue;
            foreach(var t in self.TweenAnimations)
            {
                t.DOPlayForward();
                maxDuration = Math.Max(maxDuration, t.duration);
            }

            Observable.Timer(TimeSpan.FromSeconds(maxDuration))
                .SubscribeWithState(self, (_, _self) =>
                {
                    Broker.Global.Publish(PopupEvents.CompleteOpen.Get(_self));
                });
        }

        /// <summary>
        /// ツイーンしながら非表示を開始する
        /// </summary>
        public static void StartTweeningClose(this ITweenPopup self)
        {
            Broker.Global.Publish(PopupEvents.StartClose.Get(self));

            var maxDuration = float.MinValue;
            foreach (var t in self.TweenAnimations)
            {
                t.DOPlayBackwards();
                maxDuration = Math.Max(maxDuration, t.duration);
            }

            Observable.Timer(TimeSpan.FromSeconds(maxDuration))
                .SubscribeWithState(self, (_, _self) =>
                {
                    Broker.Global.Publish(PopupEvents.CompleteClose.Get(_self));
                });
        }
    }
}
