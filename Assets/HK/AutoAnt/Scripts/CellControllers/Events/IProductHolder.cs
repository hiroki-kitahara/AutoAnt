using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers.Events
{
    /// <summary>
    /// 生産物を保持するインターフェイス
    /// </summary>
    public interface IProductHolder
    {
        /// <summary>
        /// 生産物リスト
        /// </summary>
        List<int> Products { get; }
    }
}
