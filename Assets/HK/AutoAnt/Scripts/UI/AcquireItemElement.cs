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
        private TextMeshProUGUI text;

        [SerializeField]
        private StringAsset.Finder format;

        [SerializeField]
        private float destroyDelay;

        public AcquireItemElement Initialize(MasterDataItem.Element item, int amount, Inventory inventory)
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
