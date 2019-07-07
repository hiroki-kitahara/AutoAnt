using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers.Events
{
    /// <summary>
    /// 住宅のインターフェイス
    /// </summary>
    public interface IHousing
    {
        /// <summary>
        /// 保持している人口量
        /// </summary>
        double CurrentPopulation { get; }

        /// <summary>
        /// ベースの人口増加量
        /// </summary>
        double BasePopulation { get; }
    }
}
