using System;
using DG.Tweening;
using HK.AutoAnt.Database;
using HK.AutoAnt.UserControllers;
using HK.Framework;
using HK.Framework.Text;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// 通知UIの要素を制御するクラス
    /// </summary>
    public sealed class NotificationUIElement : MonoBehaviour
    {
        public enum MessageType
        {
            Information,
            Error,
        }

        [SerializeField]
        private TextMeshProUGUI text = null;

        [SerializeField]
        private RectTransform animationTarget = null;

        [SerializeField]
        private Image background = null;

        [SerializeField]
        private float animationDuration = 1.0f;

        [SerializeField]
        private Ease animationEase = Ease.Linear;

        [SerializeField]
        private Color informationColor = Color.white;

        [SerializeField]
        private Color errorColor = Color.white;

        private ObjectPool<NotificationUIElement> pool = null;

        private RectTransform cachedTransform;

        private static readonly ObjectPoolBundle<NotificationUIElement> pools = new ObjectPoolBundle<NotificationUIElement>();

        void Awake()
        {
            this.cachedTransform = this.transform as RectTransform;
        }

        public NotificationUIElement Rent(Transform parent, string message, MessageType messageType, float delayDestroy)
        {
            var pool = pools.Get(this);
            var clone = pool.Rent();

            clone.pool = pool;
            clone.Initialize(parent, message, messageType, delayDestroy);

            return clone;
        }

        private NotificationUIElement Initialize(Transform parent, string message, MessageType messageType, float delayDestroy)
        {
            this.cachedTransform.SetParent(parent, false);
            this.cachedTransform.SetAsFirstSibling();
            LayoutRebuilder.ForceRebuildLayoutImmediate(this.cachedTransform);

            this.text.text = message;
            this.background.color = this.GetColor(messageType);

            var p = this.cachedTransform.anchoredPosition;
            p.x += this.cachedTransform.rect.width;
            p.y = 0.0f;
            this.animationTarget.anchoredPosition = p;

            this.animationTarget
                .DOAnchorPosX(this.cachedTransform.anchoredPosition.x, this.animationDuration, true)
                .SetEase(this.animationEase);

            Observable.Timer(TimeSpan.FromSeconds(delayDestroy))
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.pool.Return(_this);
                })
                .AddTo(this);

            return this;
        }

        private Color GetColor(MessageType messageType)
        {
            switch(messageType)
            {
                case MessageType.Information:
                    return this.informationColor;
                case MessageType.Error:
                    return this.errorColor;
                default:
                    Assert.IsTrue(false, $"{messageType}は未対応です");
                    return this.errorColor;
            }
        }
    }
}
