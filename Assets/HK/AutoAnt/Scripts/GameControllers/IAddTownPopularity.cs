using HK.AutoAnt.UserControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// 街の人気度を加算するインターフェイス
    /// </summary>
    public interface IAddTownPopularity
    {
        /// <summary>
        /// 人気度を加算する
        /// </summary>
        void Add(Town town);
    }
}
