using HK.AutoAnt.UI;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Events
{
    /// <summary>
    /// ポップアップ関連のイベント
    /// </summary>
    public class PopupEvents
    {
        /// <summary>
        /// 表示処理が開始した際のイベント
        /// </summary>
        public class StartOpen : Message<StartOpen, Popup>
        {
            public Popup Popup => this.param1;
        }

        /// <summary>
        /// 表示処理が完了した際のイベント
        /// </summary>
        public class CompleteOpen : Message<CompleteOpen, Popup>
        {
            public Popup Popup => this.param1;
        }

        /// <summary>
        /// 閉じる処理が開始した際のイベント
        /// </summary>
        public class StartClose : Message<StartClose, Popup>
        {
            public Popup Popup => this.param1;
        }

        /// <summary>
        /// 閉じる処理が完了した際のイベント
        /// </summary>
        public class CompleteClose : Message<CompleteClose, Popup>
        {
            public Popup Popup => this.param1;
        }
    }
}
