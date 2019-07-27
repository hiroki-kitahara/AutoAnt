using System;
using System.Collections.Generic;
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

        private readonly List<SEElement> elements = new List<SEElement>();

        public void Play(AudioClip clip, float volume)
        {
            var element = this.elementPrefab.Rent();
            this.elements.Add(element);
            element.transform.SetParent(this.transform);
            element.AudioSource.volume = volume;
            element.Play(clip);
            Observable.Timer(TimeSpan.FromSeconds(clip.length))
                .SubscribeWithState2(this, element, (_, _this, _element) =>
                {
                    _element.Return();
                    _this.elements.Remove(_element);
                })
                .AddTo(this);
        }

        public void SetVolume(float value)
        {
            foreach(var e in this.elements)
            {
                e.AudioSource.volume = value;
            }
        }
    }
}
