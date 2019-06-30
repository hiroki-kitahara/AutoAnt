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
        ClickMode,

        /// <summary>
        /// 建築モード
        /// </summary>
        BuildMode,

        /// <summary>
        /// 解体モード
        /// </summary>
        DismantleMode,

        /// <summary>
        /// 開拓モード
        /// </summary>
        ExploringMode,
    }

    public enum CellEventCategory
    {
        /// <summary>
        /// 住宅
        /// </summary>
        Housing,

        /// <summary>
        /// 農場
        /// </summary>
        Farm,

        /// <summary>
        /// 工場
        /// </summary>
        Factory,

        /// <summary>
        /// 道路
        /// </summary>
        Road,
    }
}
