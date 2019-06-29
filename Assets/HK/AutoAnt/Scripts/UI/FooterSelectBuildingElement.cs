using HK.AutoAnt.Database;
using HK.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// フッターメニューの建設メニューの要素を制御するクラス
    /// </summary>
    public sealed class FooterSelectBuildingElement : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI text = null;
        
        private readonly ObjectPoolBundle<FooterSelectBuildingElement> pools = new ObjectPoolBundle<FooterSelectBuildingElement>();

        private ObjectPool<FooterSelectBuildingElement> pool;

        public FooterSelectBuildingElement Clone(MasterDataCellEvent.Record record)
        {
            var pool = pools.Get(this);
            var clone = pool.Rent();
            clone.pool = pool;

            clone.text.text = record.Id.ToString();

            return clone;
        }

        public void ReturnToPool()
        {
            this.pool.Return(this);
        }
    }
}
