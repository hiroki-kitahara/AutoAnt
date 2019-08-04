using HK.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace HK.AutoAnt.UI.Elements
{
    /// <summary>
    /// <see cref="GridList"/>の要素を制御するクラス
    /// </summary>
    public sealed class GridListElement : MonoBehaviour
    {
        [SerializeField]
        private Image background = null;
        public Image Background => this.background;

        [SerializeField]
        private Image value = null;
        public Image Value => this.value;

        [SerializeField]
        private TextMeshProUGUI amount = null;
        public TextMeshProUGUI Amount => this.amount;

        [SerializeField]
        private Button button = null;
        public Button Button => this.button;

        private static readonly ObjectPoolBundle<GridListElement> pools = new ObjectPoolBundle<GridListElement>();

        private ObjectPool<GridListElement> pool;

        private static Transform pooledParent;

        public GridListElement Rent()
        {
            var pool = pools.Get(this);
            var clone = pool.Rent();
            clone.pool = pool;

            return clone;
        }

        public void Return()
        {
            this.transform.SetParent(pooledParent);
            this.pool.Return(this);
        }

        private static void Initialize()
        {
            if(pooledParent != null)
            {
                return;
            }

            pooledParent = new GameObject("PooledGridListElement").transform;
            DontDestroyOnLoad(pooledParent);
        }
    }
}
