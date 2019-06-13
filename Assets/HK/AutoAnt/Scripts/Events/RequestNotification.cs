﻿using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Events
{
    /// <summary>
    /// 通知UIにメッセージをリクエストするイベント
    /// </summary>
    /// <remarks>
    /// デバッグ用に使う想定です
    /// </remarks>
    public sealed class RequestNotification : Message<RequestNotification, string>
    {
        /// <summary>
        /// 表示したいメッセージ
        /// </summary>
        public string Message => this.param1;
    }
}