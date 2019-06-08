using System;
using HK.Framework;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.EffectSystems
{
    /// <summary>
    /// Pool可能なエフェクト
    /// </summary>
    public sealed class PoolableEffect : MonoBehaviour
    {
        /// <summary>
        /// Poolするまでの遅延時間（秒）
        /// </summary>
        [SerializeField]
        private float delayReturnToPool = 0.0f;

        private static ObjectPoolBundle<PoolableEffect> pools = new ObjectPoolBundle<PoolableEffect>();

        private ObjectPool<PoolableEffect> pool;

        private IDisposable returnStream;

        public PoolableEffect Rent()
        {
            var pool = pools.Get(this);
            var result = pool.Rent();
            result.pool = pool;

            if(result.returnStream != null)
            {
                result.returnStream.Dispose();
                result.returnStream = null;
            }

            result.returnStream = Observable.Timer(TimeSpan.FromSeconds(result.delayReturnToPool))
                .SubscribeWithState(result, (_, _result) =>
                {
                    _result.pool.Return(_result);
                })
                .AddTo(result);

            return result;
        }
    }
}
