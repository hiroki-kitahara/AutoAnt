using System;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// ポップアップのインターフェイス
    /// </summary>
    public interface IPopup
    {
        /// <summary>
        /// ポップアップのイベントを通知するブローカー
        /// </summary>
        /// <value></value>
        IMessageBroker Broker { get; }

        /// <summary>
        /// 開く
        /// </summary>
        void Open();

        /// <summary>
        /// 閉じる
        /// </summary>
        void Close();
    }
}
