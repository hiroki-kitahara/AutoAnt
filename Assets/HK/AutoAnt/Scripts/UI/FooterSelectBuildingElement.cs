using HK.AutoAnt.Database;
using HK.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UniRx;
using UnityEngine.UI;
using HK.Framework.EventSystems;
using HK.AutoAnt.Events;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// フッターメニューの建設メニューの要素を制御するクラス
    /// </summary>
    public sealed class FooterSelectBuildingElement : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI text = null;

        [SerializeField]
        private Button button = null;

        private readonly ObjectPoolBundle<FooterSelectBuildingElement> pools = new ObjectPoolBundle<FooterSelectBuildingElement>();

        private ObjectPool<FooterSelectBuildingElement> pool;

        private MasterDataCellEvent.Record record;

        void Awake()
        {
            Assert.IsNotNull(this.button);

            this.button.OnClickAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    Assert.IsNotNull(_this.record);

                    Broker.Global.Publish(RequestBuildingMode.Get(_this.record.Id));
                })
                .AddTo(this);
        }

        public FooterSelectBuildingElement Rent(MasterDataCellEvent.Record record)
        {
            var pool = pools.Get(this);
            var clone = pool.Rent();
            clone.pool = pool;

            clone.text.text = record.Id.ToString();
            clone.record = record;

            return clone;
        }

        public void ReturnToPool()
        {
            this.pool.Return(this);
        }
    }
}
