using HK.AutoAnt.CellControllers.Events;
using HK.AutoAnt.Events;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// 生産物吹き出しUIを制御するクラス
    /// </summary>
    public sealed class ProductSpeechBubbleController : MonoBehaviour
    {
        [SerializeField]
        private ProductSpeechBubbleElement elementPrefab;

        void Awake()
        {
            Broker.Global.Receive<AddedCellEvent>()
                .Where(x => x.CellEvent is Facility)
                .SubscribeWithState(this, (x, _this) =>
                {
                    Instantiate(_this.elementPrefab, _this.transform, false).Initialize(x.CellEvent as Facility);
                })
                .AddTo(this);
        }
    }
}
