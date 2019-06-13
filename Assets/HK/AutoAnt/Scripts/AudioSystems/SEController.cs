using System;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.AudioSystems
{
    /// <summary>
    /// SEを制御するクラス
    /// </summary>
    public sealed class SEController : MonoBehaviour
    {
        [SerializeField]
        private SEElement elementPrefab = null;

        public void Play(AudioClip clip)
        {
            var element = this.elementPrefab.Rent();
            element.transform.SetParent(this.transform);
            element.Play(clip);
            Observable.Timer(TimeSpan.FromSeconds(clip.length))
                .SubscribeWithState(element, (_, _element) =>
                {
                    _element.Return();
                })
                .AddTo(this);
        }
    }
}
