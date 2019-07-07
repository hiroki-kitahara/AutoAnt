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
    /// ヘッダーの人口UIを制御するクラス
    /// </summary>
    public sealed class HeaderPopulationController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI value = null;

        [SerializeField]
        private StringAsset.Finder format = null;

        private double cachedPopulation;

        void Start()
        {
            GameSystem.Instance.UpdateAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    var population = GameSystem.Instance.User.Town.Population.Value;
                    if(_this.cachedPopulation == population)
                    {
                        return;
                    }

                    _this.cachedPopulation = population;
                    _this.value.text = _this.format.Format(population.ToReadableString("###.00"));
                })
                .AddTo(this);
        }
    }
}
