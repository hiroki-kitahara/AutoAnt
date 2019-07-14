using HK.AutoAnt.CellControllers.Events;
using UnityEngine;
using UnityEngine.Assertions;
using HK.AutoAnt.Events;
using UniRx;
using HK.AutoAnt.Database;
using HK.AutoAnt.Systems;
using HK.AutoAnt.Extensions;

namespace HK.AutoAnt.CellControllers.Gimmicks
{
    /// <summary>
    /// 生産物吹き出しを制御するクラス
    /// </summary>
    public sealed class ProductSpeechBubbleController : MonoBehaviour, ICellEventGimmick
    {
        [SerializeField]
        private GameObject target = null;

        [SerializeField]
        private Renderer productRenderer = null;

        public void Attach(CellEvent cellEvent)
        {
            var productHolder = cellEvent as IProductHolder;
            Assert.IsNotNull(productHolder);

            // 既に何か生産している場合は吹き出しを即座に表示する
            if(productHolder.Products.Count > 0)
            {
                var productId = productHolder.Products[productHolder.Products.Count - 1];
                var record = GameSystem.Instance.MasterData.Item.Records.Get(productId);
                this.Apply(record);
            }
            else
            {
                this.Hidden();
            }

            cellEvent.Broker.Receive<AddedFacilityProduct>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.Apply(x.Product);
                })
                .AddTo(this);

            cellEvent.Broker.Receive<AcquiredFacilityProduct>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.Hidden();
                })
                .AddTo(this);
        }

        public void Detach(CellEvent cellEvent)
        {
        }

        /// <summary>
        /// 生産物のアイコンを表示する
        /// </summary>
        private void Apply(MasterDataItem.Record record)
        {
            this.target.SetActive(true);
            this.productRenderer.material.mainTexture = record.Icon;
        }

        /// <summary>
        /// 吹き出しを非表示にする
        /// </summary>
        private void Hidden()
        {
            this.target.SetActive(false);
        }
    }
}
