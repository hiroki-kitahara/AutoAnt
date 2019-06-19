using System;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// ポップアップの抽象クラス
    /// </summary>
    public abstract class Popup : MonoBehaviour, IPopup
    {
        protected readonly Subject<int> response = new Subject<int>();

        protected readonly Subject<Unit> close = new Subject<Unit>();

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

            this.close.OnNext(Unit.Default);
        }

        /// <summary>
        /// ポップアップのレスポンスを返す
        /// </summary>
        public virtual IObservable<int> ResponseAsObservable()
        {
            return this.response;
        }

        /// <summary>
        /// 閉じた際のイベント
        /// </summary>
        public IObservable<Unit> CloseAsObservable()
        {
            return this.close;
        }
    }
}
