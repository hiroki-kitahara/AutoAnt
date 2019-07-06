using System;
using DG.Tweening;
using HK.AutoAnt.Events;
using HK.AutoAnt.Extensions;
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
    public sealed class SimplePopup : TweenPopup
    {
        [SerializeField]
        private TextMeshProUGUI message = null;

        [SerializeField]
        private ButtonElement decide = null;
        public Button DecideButton => this.decide.Button;

        [SerializeField]
        private ButtonElement cancel = null;
        public Button CancelButton => this.cancel.Button;

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

        [Serializable]
        public class ButtonElement
        {
            public Button Button;

            public TextMeshProUGUI Text;
        }
    }
}
