using System;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Assertions;
using UniRx;

namespace HK.AutoAnt.Advertisements
{
    /// <summary>
    /// 広告処理を制御するクラス
    /// </summary>
    public class AutoAntAdvertisement : MonoBehaviour
    {
#if UNITY_IOS
        private const string GameId = "3215731";
#elif UNITY_ANDROID
        private const string GameId = "3215730";
#endif

        /// <summary>
        /// 広告を表示しているか返す
        /// </summary>
        /// <remarks>
        /// <see cref="UnityEngine.Advertisements.Advertisement.isShowing"/>と違って広告表示開始前から<c>true</c>を返します
        /// </remarks>
        public bool IsShow { get; private set; } = false;

        void Awake()
        {
#if (UNITY_IOS || UNITY_ANDROID) && AA_DEBUG
            var testMode = true;
#else
            var testMode = false;
#endif

#if UNITY_IOS || UNITY_ANDROID
            Advertisement.Initialize(GameId, testMode);
#endif
        }

        public IObservable<ShowResult> Show()
        {
            return Observable.Create<ShowResult>(observer =>
            {
                Observable.EveryUpdate()
                    .Where(_ => Advertisement.IsReady())
                    .Take(1)
                    .SubscribeWithState(observer, (_, _observer) =>
                    {
                        var showOptions = new ShowOptions();
                        showOptions.resultCallback = (showResult) =>
                        {
                            this.IsShow = false;
                            _observer.OnNext(showResult);
                            _observer.OnCompleted();
                        };

                        this.IsShow = true;
                        Advertisement.Show(showOptions);
                    });

                return Disposable.Empty;
            });
        }
    }
}
