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
        private TextMeshProUGUI value;

        [SerializeField]
        private StringAsset.Finder format;

        void Start()
        {
            GameSystem.Instance.User.Wallet.MoneyAsObservable
                .SubscribeWithState(this, (money, _this) =>
                {
                    _this.value.text = _this.format.Format(money.ToString());
                })
                .AddTo(this);
        }
    }
}
