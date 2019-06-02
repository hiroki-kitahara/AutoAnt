using System;
using System.Collections.Generic;
using HK.Framework.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers
{
    /// <summary>
    /// セルイベントのレベルアップに必要なコスト
    /// </summary>
    [Serializable]
    public sealed class LevelUpCost
    {
        /// <summary>
        /// お金
        /// </summary>
        [SerializeField]
        private int money;

        /// <summary>
        /// 必要なアイテムリスト
        /// </summary>
        [SerializeField]
        private List<NeedItem> needItems = new List<NeedItem>();

        /// <summary>
        /// 必要なアイテム
        /// </summary>
        [Serializable]
        public class NeedItem
        {
            /// <summary>
            /// アイテムの名前
            /// </summary>
            [SerializeField]
            private StringAsset.Finder itemName;

            /// <summary>
            /// 必要な量
            /// </summary>
            [SerializeField]
            private int amount;
        }
    }
}
