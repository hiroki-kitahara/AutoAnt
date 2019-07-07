using HK.AutoAnt.Extensions;
using HK.AutoAnt.Systems;
using HK.Framework.Text;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// ヘッダーのお金UIを制御するクラス
    /// </summary>
    public sealed class HeaderMoneyController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI value = null;

        [SerializeField]
        private StringAsset.Finder format = null;

        private double cachedMoney;

        void Awake()
        {
            GameSystem.Instance.UpdateAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    var money = GameSystem.Instance.User.Wallet.Money;
                    if(_this.cachedMoney == money)
                    {
                        return;
                    }

                    _this.cachedMoney = money;
                    _this.value.text = _this.format.Format(money.ToReadableString("###.00"));
                })
                .AddTo(this);
        }
    }
}
