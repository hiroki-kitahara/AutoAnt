using System;
using HK.AutoAnt.Database;
using HK.AutoAnt.UserControllers;
using HK.Framework.Text;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// アイテム取得UIの要素を制御するクラス
    /// </summary>
    public sealed class AcquireItemElement : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI text = null;

        [SerializeField]
        private StringAsset.Finder format = null;

        [SerializeField]
        private float destroyDelay = 0.0f;

        public AcquireItemElement Initialize(MasterDataItem.Record item, int amount, Inventory inventory)
        {
            this.text.text = this.format.Format(item.Name, amount, inventory.Items[item.Id]);

            Observable.Timer(TimeSpan.FromSeconds(this.destroyDelay))
                .SubscribeWithState(this, (_, _this) =>
                {
                    Destroy(_this.gameObject);
                })
                .AddTo(this);

            return this;
        }
    }
}
