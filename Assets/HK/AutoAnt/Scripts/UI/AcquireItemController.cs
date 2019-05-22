using HK.AutoAnt.Database;
using HK.AutoAnt.Events;
using HK.AutoAnt.UserControllers;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// アイテム取得UIを制御するクラス
    /// </summary>
    public sealed class AcquireItemController : MonoBehaviour
    {
        [SerializeField]
        private AcquireItemElement elementPrefab = null;

        void Awake()
        {
            Broker.Global.Receive<AddedItem>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.CreateElement(x.Inventory, x.Item, x.Amount);
                })
                .AddTo(this);
        }

        private void CreateElement(Inventory inventory, MasterDataItem.Record item, int amount)
        {
            Instantiate(this.elementPrefab, this.transform, false).Initialize(item, amount, inventory);
        }
    }
}
