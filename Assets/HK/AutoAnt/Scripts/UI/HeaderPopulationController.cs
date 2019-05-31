using HK.AutoAnt.Systems;
using HK.Framework.Text;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// ヘッダーの人口UIを制御するクラス
    /// </summary>
    public sealed class HeaderPopulationController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI value = null;

        [SerializeField]
        private StringAsset.Finder format = null;

        void Start()
        {
            GameSystem.Instance.User.Town.PopulationAsObservable
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.value.text = _this.format.Format(x.ToString());
                })
                .AddTo(this);
        }
    }
}
