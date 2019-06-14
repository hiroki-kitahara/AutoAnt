using HK.AutoAnt.Extensions;
using HK.AutoAnt.Systems;
using HK.Framework.Text;
using TMPro;
using UniRx;
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

        void Start()
        {
            GameSystem.Instance.User.Wallet.MoneyAsObservable
                .SubscribeWithState(this, (money, _this) =>
                {
                    _this.value.text = _this.format.Format(money.ToReadableString());
                })
                .AddTo(this);
        }
    }
}
