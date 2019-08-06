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

        /// <summary>
        /// 貯蔵
        /// </summary>
        Chest,
    }

    /// <summary>
    /// セルイベントを生成可能かの評価タイプ
    /// </summary>
    public enum CellEventGenerateEvalute
    {
        /// <summary>
        /// 建設可能
        /// </summary>
        Possible,

        /// <summary>
        /// セルが無い
        /// </summary>
        NotCell,

        /// <summary>
        /// 既に別のセルイベントがある
        /// </summary>
        AlreadyExistsCellEvent,

        /// <summary>
        /// コストが足りない
        /// </summary>
        NotEnoughCost,

        /// <summary>
        /// セルイベントごとの条件を満たしていない
        /// </summary>
        NotEnoughCondition,
    }

    /// <summary>
    /// ゲームを離れた理由
    /// </summary>
    public enum GameLeftCase
    {
        /// <summary>
        /// 正常に離れた
        /// </summary>
        /// <remarks>
        /// 普通にアプリを終了した場合などはこれになります
        /// </remarks>
        Normal,

        /// <summary>
        /// 広告によって離れた
        /// </summary>
        ByAdvertisement,
    }
}
