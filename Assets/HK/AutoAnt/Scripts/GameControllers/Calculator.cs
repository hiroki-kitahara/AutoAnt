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
    }
}
