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
    /// ヘッダーの人気度UIを制御するクラス
    /// </summary>
    public sealed class HeaderPopularityController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI value = null;

        [SerializeField]
        private StringAsset.Finder format = null;

        private double cachedPopularity;

        void Start()
        {
            GameSystem.Instance.UpdateAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    var popularity = GameSystem.Instance.User.Town.Popularity.Value;
                    if(_this.cachedPopularity == popularity)
                    {
                        return;
                    }

                    _this.cachedPopularity = popularity;
                    _this.value.text = _this.format.Format(popularity.ToReadableString("###.00"));
                })
                .AddTo(this);
        }
    }
}
