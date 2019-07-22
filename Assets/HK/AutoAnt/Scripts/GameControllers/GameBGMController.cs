using HK.AutoAnt.AudioSystems;
using HK.AutoAnt.Systems;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// ゲームで利用するBGMを制御するクラス
    /// </summary>
    public sealed class GameBGMController : MonoBehaviour
    {
        [SerializeField]
        private BGMController.ClipBundle clipBundle = null;

        void Awake()
        {
            GameSystem.Instance.User.Option.BGMVolume
                .SubscribeWithState(this, (x, _this) =>
                {
                    AutoAntSystem.Audio.BGM.AudioSource.volume = x;
                })
                .AddTo(this);
        }

        void Start()
        {
            AutoAntSystem.Audio.BGM.Play(this.clipBundle);
        }
    }
}
