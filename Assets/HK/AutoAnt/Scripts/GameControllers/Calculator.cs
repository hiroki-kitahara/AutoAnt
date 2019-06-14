using HK.AutoAnt.Systems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// ゲームに関する様々な計算式を定義するクラス
    /// </summary>
    public static class Calculator
    {
        /// <summary>
        /// セルを開拓するために必要なお金を返す
        /// </summary>
        public static int DevelopCost(GameSystem gameSystem, Vector2Int position)
        {
            return gameSystem.Constants.Cell.DevelopCost * position.sqrMagnitude;
        }

        /// <summary>
        /// 人口増加量を返す
        /// </summary>
        /// <param name="basePopulation">ベースの増加量</param>
        /// <param name="popularity">人気度</param>
        /// <param name="popularityRate">人気度の係数</param>
        public static int AddPopulation(int basePopulation, int popularity, float popularityRate)
        {
            return Mathf.FloorToInt(basePopulation * (popularity / popularityRate));
        }
    }
}
