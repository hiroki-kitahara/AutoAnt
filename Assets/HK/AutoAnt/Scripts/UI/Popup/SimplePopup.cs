using System;
using HK.AutoAnt.Events;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// メッセージとボタンのみのポップアップを制御するクラス
    /// </summary>
    public sealed class SimplePopup : Popup
    {
        [SerializeField]
        private TextMeshProUGUI message = null;

        [SerializeField]
        private ButtonElement decide = null;

        [SerializeField]
        private ButtonElement cancel = null;

        private int decideValue = 1;

        private int cancelValue = 2;

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="message">表示したいメッセージ</param>
        /// <param name="decide">決定ボタンのテキスト</param>
        /// <param name="cancel">キャンセルボタンのテキスト</param>
        public SimplePopup Initialize(string message, string decide, string cancel)
        {
            this.message.text = message;
            this.decide.Text.text = decide;
            this.cancel.Text.text = cancel;

            this.decide.Button.OnClickAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.Broker.Publish(PopupEvents.Response.Get(_this.decideValue));
                })
                .AddTo(this);

            this.cancel.Button.OnClickAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.Broker.Publish(PopupEvents.Response.Get(_this.cancelValue));
                })
                .AddTo(this);

            return this;
        }

        /// <summary>
        /// 決定ボタンのみのレイアウトで初期化する
        /// </summary>
        /// <param name="message">表示したいメッセージ</param>
        /// <param name="decide">決定ボタンのテキスト</param>
        public SimplePopup Initialize(string message, string decide)
        {
            this.Initialize(message, decide, "");

            this.cancel.Button.gameObject.SetActive(false);

            return this;
        }

        /// <summary>
        /// 決定ボタンを押した際のレスポンス値を設定する
        /// </summary>
        public SimplePopup DecideValue(int value)
        {
            this.decideValue = value;

            return this;
        }

        /// <summary>
        /// キャンセルボタンを押した際のレスポンス値を設定する
        /// </summary>
        public SimplePopup CancelValue(int value)
        {
            this.cancelValue = value;

            return this;
        }

        [Serializable]
        public class ButtonElement
        {
            public Button Button;

            public TextMeshProUGUI Text;
        }
    }
}
