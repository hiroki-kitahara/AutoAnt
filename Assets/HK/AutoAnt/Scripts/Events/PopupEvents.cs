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
        /// 閉じる処理が開始した際のイベント
        /// </summary>
        public class StartClose : Message<StartClose>
        {
        }

        /// <summary>
        /// 閉じる処理が完了した際のイベント
        /// </summary>
        public class CompleteClose : Message<CompleteClose>
        {
        }
    }
}
