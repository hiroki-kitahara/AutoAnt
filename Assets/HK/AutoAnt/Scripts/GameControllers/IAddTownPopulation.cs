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
        /// 人口を加算する
        /// </summary>
        void Add(Town town);
    }
}
