using System;
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
            this.gameObject.SetActive(true);
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        public virtual void Close()
        {
            this.gameObject.SetActive(false);
        }

        /// <summary>
        /// ポップアップのレスポンスを返す
        /// </summary>
        public abstract IObservable<int> ResponseAsObservable();
    }
}
