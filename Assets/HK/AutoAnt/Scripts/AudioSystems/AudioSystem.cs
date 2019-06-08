using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.AudioSystems
{
    /// <summary>
    /// オーディオシステム
    /// </summary>
    public sealed class AudioSystem : MonoBehaviour
    {
        [SerializeField]
        private BGMController bgm;
        public BGMController BGM => this.bgm;
    }
}
