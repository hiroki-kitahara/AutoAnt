using HK.Framework;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.AudioSystems
{
    /// <summary>
    /// SEを再生する要素を持つクラス
    /// </summary>
    public sealed class SEElement : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource = null;
        public AudioSource AudioSource => this.audioSource;

        private static readonly ObjectPoolBundle<SEElement> pools = new ObjectPoolBundle<SEElement>();

        private ObjectPool<SEElement> pool;

        public SEElement Rent()
        {
            Assert.IsNotNull(pools);

            var pool = pools.Get(this);
            var result = pool.Rent();
            result.pool = pool;

            return result;
        }

        public void Return()
        {
            Assert.IsNotNull(this.pool);

            this.pool.Return(this);
        }

        public void Play(AudioClip clip)
        {
            Assert.IsNotNull(clip);
            Assert.IsNotNull(this.audioSource);
            
            this.audioSource.PlayOneShot(clip);
        }
    }
}
