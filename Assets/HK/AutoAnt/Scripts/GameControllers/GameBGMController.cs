using HK.AutoAnt.AudioSystems;
using HK.AutoAnt.Systems;
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
        private BGMController.ClipBundle clipBundle;

        void Start()
        {
            AutoAntSystem.Audio.BGM.Play(this.clipBundle);
        }
    }
}
