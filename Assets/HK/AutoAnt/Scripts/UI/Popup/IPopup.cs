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
        /// 開く
        /// </summary>
        void Open();

        /// <summary>
        /// 閉じる
        /// </summary>
        void Close();

        /// <summary>
        /// ポップアップのレスポンスを返す
        /// </summary>
        IObservable<int> ResponseAsObservable();

        /// <summary>
        /// 閉じた際のイベント
        /// </summary>
        IObservable<Unit> CloseAsObservable();
    }
}
