using System;
using System.Collections.Generic;
using HK.AutoAnt.Events;
using HK.AutoAnt.Systems;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UserControllers
{
    /// <summary>
    /// 生成可能なセルイベントを管理するクラス
    /// </summary>
    [Serializable]
    public sealed class UnlockCellEvents
    {
        /// <summary>
        /// 生成可能なセルイベントリスト
        /// </summary>
        public List<int> Elements { get; private set; } = new List<int>();
    }
}
