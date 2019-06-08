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
        private AudioSource audioSource;

        private static readonly ObjectPoolBundle<SEElement> pools = new ObjectPoolBundle<SEElement>();

        private ObjectPool<SEElement> pool;

        public SEElement Rent()
        {
            var pool = pools.Get(this);
            var result = pool.Rent();
            result.pool = pool;

            return result;
        }

        public void Return()
        {
            this.pool.Return(this);
        }

        public void Play(AudioClip clip)
        {
            this.audioSource.PlayOneShot(clip);
        }
    }
}
