using HK.AutoAnt.CellControllers.Events;
using UnityEngine;
using UnityEngine.Assertions;
using HK.AutoAnt.Events;
using UniRx;

namespace HK.AutoAnt.CellControllers.Gimmicks
{
    /// <summary>
    /// 生産物吹き出しを制御するクラス
    /// </summary>
    public sealed class ProductSpeechBubbleController : MonoBehaviour, ICellEventGimmick
    {
        [SerializeField]
        private GameObject target = null;

        public void Attach(CellEvent cellEvent)
        {
            cellEvent.Broker.Receive<AddedFacilityProduct>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.target.SetActive(true);
                })
                .AddTo(this);

            cellEvent.Broker.Receive<AcquiredFacilityProduct>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.target.SetActive(false);
                })
                .AddTo(this);

            this.target.SetActive(false);
        }
    }
}
