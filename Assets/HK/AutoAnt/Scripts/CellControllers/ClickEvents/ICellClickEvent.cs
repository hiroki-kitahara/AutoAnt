﻿using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers.ClickEvents
{
    /// <summary>
    /// <see cref="Cell"/>をクリックした際のイベントのインターフェイス
    /// </summary>
    public interface ICellClickEvent
    {
        GameObject Prefab { get; }
        
        /// <summary>
        /// イベントを実行する
        /// </summary>
        void Do(Cell owner);
    }
}
