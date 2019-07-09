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

        public static IObservable<ShowResult> Show()
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
                            _observer.OnNext(showResult);
                        };

                        Advertisement.Show(showOptions);
                    });

                return Disposable.Empty;
            });
        }
    }
}
