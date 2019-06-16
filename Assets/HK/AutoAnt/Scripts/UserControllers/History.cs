using System;
using System.Collections.Generic;
using System.Linq;
using HK.AutoAnt.Database;
using HK.AutoAnt.Events;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UserControllers
{
    /// <summary>
    /// ユーザーの行動履歴
    /// </summary>
    [Serializable]
    public sealed class History
    {
        /// <summary>
        /// ゲーム関連の履歴
        /// </summary>
        public GameHistory Game => this.game;
        private GameHistory game = new GameHistory();

        /// <summary>
        /// セルイベントの生成履歴
        /// </summary>
        public GenerateCellEventHistory GenerateCellEvent => this.generateCellEvent;
        private GenerateCellEventHistory generateCellEvent = new GenerateCellEventHistory();
    }
}
