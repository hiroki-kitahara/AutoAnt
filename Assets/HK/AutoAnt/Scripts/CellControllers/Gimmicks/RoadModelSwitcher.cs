using HK.AutoAnt.CellControllers.Events;
using HK.Framework;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers.Gimmicks
{
    /// <summary>
    /// 道路のモデルを切り替えるクラス
    /// </summary>
    public sealed class RoadModelSwitcher : MonoBehaviour, ICellEventGimmick
    {
        [SerializeField]
        private PoolableObject straight;

        [SerializeField]
        private PoolableObject turn;

        [SerializeField]
        private PoolableObject junctionT;

        [SerializeField]
        private PoolableObject crossroads;

        private PoolableObject currentObject;

        private ObjectPool<PoolableObject> currentPool;

        private static readonly ObjectPoolBundle<PoolableObject> pools = new ObjectPoolBundle<PoolableObject>();

        public void Attach(CellEvent cellEvent)
        {
            this.currentPool = pools.Get(this.straight);
            this.currentObject = this.currentPool.Rent();
            this.currentObject.transform.SetParent(this.transform);
            this.currentObject.transform.localPosition = Vector3.zero;
        }
    }
}
