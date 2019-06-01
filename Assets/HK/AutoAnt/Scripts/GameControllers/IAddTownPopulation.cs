using HK.AutoAnt.UserControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// 街の人口を加算するインターフェイス
    /// </summary>
    public interface IAddTownPopulation
    {
        /// <summary>
        /// 加算する人口の量を返す
        /// </summary>
        int GetAmount(Town town);
    }
}
