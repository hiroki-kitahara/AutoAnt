using System;
using HK.AutoAnt.Database;
using HK.AutoAnt.UserControllers;
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

        public NotificationUIElement Initialize(string message, float delayDestroy)
        {
            this.text.text = message;

            Observable.Timer(TimeSpan.FromSeconds(delayDestroy))
                .SubscribeWithState(this, (_, _this) =>
                {
                    Destroy(_this.gameObject);
                })
                .AddTo(this);

            return this;
        }
    }
}
