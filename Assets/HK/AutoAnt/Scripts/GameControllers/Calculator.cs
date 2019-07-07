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
        public static double DevelopCost(GameSystem gameSystem, Vector2Int position)
        {
            return gameSystem.Constants.Cell.DevelopCost * position.sqrMagnitude;
        }

        /// <summary>
        /// 人口増加量を返す
        /// </summary>
        /// <param name="basePopulation">ベースの増加量</param>
        /// <param name="popularity">人気度</param>
        /// <param name="popularityRate">人気度の係数</param>
        public static double AddPopulation(double basePopulation, double popularity, float popularityRate, float rate, float deltaTime)
        {
            return ((basePopulation * (popularity / popularityRate)) * rate) * deltaTime;
        }

        /// <summary>
        /// 加算出来る税金を返す
        /// </summary>
        public static double Tax(double population, float deltaTime)
        {
            // 人口は小数点以下は切り取り
            population = System.Math.Floor(population);
            
            return (population * 10.0d) * deltaTime;
        }
    }
}
