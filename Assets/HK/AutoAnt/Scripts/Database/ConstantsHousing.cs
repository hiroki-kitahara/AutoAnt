using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Database
{
    /// <summary>
    /// 住宅に関する定数
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Database/ConstantsHousing")]
    public sealed class ConstantsHousing : ScriptableObject
    {
        /// <summary>
        /// 人気度係数
        /// </summary>
        [SerializeField]
        private float popularityRate = 1000.0f;
        public float PopularityRate => this.popularityRate;
    }
}
