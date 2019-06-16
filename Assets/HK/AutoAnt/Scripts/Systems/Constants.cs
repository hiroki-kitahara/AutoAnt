using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Constants
{
    /// <summary>
    /// セルタイプ
    /// </summary>
    public enum CellType
    {
        /// <summary>
        /// 空白
        /// </summary>
        Blank,

        /// <summary>
        /// 草原
        /// </summary>
        Grassland,
    }

    public enum InputMode
    {
        /// <summary>
        /// クリックモード
        /// </summary>
        clickMode,

        /// <summary>
        /// 建築モード
        /// </summary>
        buildMode,

        /// <summary>
        /// 解体モード
        /// </summary>
        dismantleMode,

        /// <summary>
        /// 開拓モード
        /// </summary>
        exploringMode,
    }
}
