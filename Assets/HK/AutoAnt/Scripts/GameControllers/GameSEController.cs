using HK.AutoAnt.AudioSystems;
using HK.AutoAnt.Systems;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// ゲームで利用するSEを制御するクラス
    /// </summary>
    public sealed class GameSEController : MonoBehaviour
    {
        void Awake()
        {
            GameSystem.Instance.User.Option.SEVolume
                .SubscribeWithState(this, (x, _this) =>
                {
                    AutoAntSystem.Audio.SE.SetVolume(x);
                })
                .AddTo(this);
        }

        public void Play(AudioClip clip)
        {
            AutoAntSystem.Audio.SE.Play(clip, GameSystem.Instance.User.Option.SEVolume.Value);
        }
    }
}
