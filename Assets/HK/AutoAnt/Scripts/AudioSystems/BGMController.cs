using UnityEngine;
using UnityEngine.Assertions;
using UniRx;
using System;

namespace HK.AutoAnt.AudioSystems
{
    /// <summary>
    /// BGMを制御するクラス
    /// </summary>
    public sealed class BGMController : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;

        private readonly CompositeDisposable compositeDisposable = new CompositeDisposable();

        public void Play(ClipBundle clipBundle)
        {
            this.Play(clipBundle.Intro, clipBundle.Loop);
        }

        public void Play(AudioClip intro, AudioClip loop)
        {
            this.compositeDisposable.Clear();
            this.audioSource.clip = intro;
            this.audioSource.Play();
            this.audioSource.loop = false;

            Observable.Timer(TimeSpan.FromSeconds(intro.length))
                .SubscribeWithState2(this, loop, (_, _this, _loop) =>
                {
                    _this.audioSource.clip = _loop;
                    _this.audioSource.loop = true;
                    _this.audioSource.Play();
                })
                .AddTo(this)
                .AddTo(this.compositeDisposable);
        }

        [Serializable]
        public class ClipBundle
        {
            [SerializeField]
            private AudioClip intro;
            public AudioClip Intro => this.intro;

            [SerializeField]
            private AudioClip loop;
            public AudioClip Loop => this.loop;
        }
    }
}
