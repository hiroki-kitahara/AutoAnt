using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.InputControllers
{
    /// <summary>
    /// 入力のイベント群
    /// </summary>
    public static class Events
    {
        /// <summary>
        /// クリック時のイベント
        /// </summary>
        public class Click : Message<Click, int>
        {
            public int ButtonId { get { return this.param1; } }
        }

        /// <summary>
        /// クリックアップ時のイベント
        /// </summary>
        public class ClickUp : Message<ClickUp, int>
        {
            public int ButtonId { get { return this.param1; } }
        }

        /// <summary>
        /// クリックダウン時のイベント
        /// </summary>
        public class ClickDown : Message<ClickDown, int>
        {
            public int ButtonId { get { return this.param1; } }
        }

        /// <summary>
        /// ドラッグ時のイベント
        /// </summary>
        public class Drag : Message<Drag, Vector3>
        {
            public Vector3 DeltaPosition { get { return this.param1; } }
        }
    }
}
