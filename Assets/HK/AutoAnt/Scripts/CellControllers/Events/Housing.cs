using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers.Events
{
    /// <summary>
    /// 住宅のセルイベント
    /// </summary>
    /// <remarks>
    /// - やっていること
    ///     - n秒間隔で人口が増加する
    /// </remarks>
    [CreateAssetMenu(menuName = "AutoAnt/Cell/Event/Housing")]
    public sealed class Housing : CellEventBlankGimmick
    {
        /// <summary>
        /// ベース人口増加量
        /// </summary>
        public int BasePopulationAmount;

        /// <summary>
        /// 保持している人口
        /// </summary>
        [HideInInspector]
        public int CurrentPopulation;

        /// <summary>
        /// レベル
        /// </summary>
        /// <remarks>
        /// 人口増加の変動に利用しています
        /// </remarks>
        [HideInInspector]
        public int Level;
    }
}
