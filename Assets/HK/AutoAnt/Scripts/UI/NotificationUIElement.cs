using System;
using HK.AutoAnt.Database;
using HK.AutoAnt.UserControllers;
using HK.Framework;
using HK.Framework.Text;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// 通知UIの要素を制御するクラス
    /// </summary>
    public sealed class NotificationUIElement : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI text = null;

        private ObjectPool<NotificationUIElement> pool = null;

        private static readonly ObjectPoolBundle<NotificationUIElement> pools = new ObjectPoolBundle<NotificationUIElement>();

        public NotificationUIElement Rent(string message, float delayDestroy)
        {
            var pool = pools.Get(this);
            var clone = pool.Rent();

            clone.pool = pool;
            clone.Initialize(message, delayDestroy);

            return clone;
        }

        private NotificationUIElement Initialize(string message, float delayDestroy)
        {
            this.text.text = message;

            Observable.Timer(TimeSpan.FromSeconds(delayDestroy))
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.pool.Return(_this);
                })
                .AddTo(this);

            return this;
        }
    }
}
