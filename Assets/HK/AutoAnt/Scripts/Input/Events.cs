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
        public class Click : Message<Click, ClickData>
        {
            public ClickData Data { get { return this.param1; } }
        }

        /// <summary>
        /// クリックアップ時のイベント
        /// </summary>
        public class ClickUp : Message<ClickUp, ClickData>
        {
            public ClickData Data { get { return this.param1; } }
        }

        /// <summary>
        /// クリックダウン時のイベント
        /// </summary>
        public class ClickDown : Message<ClickDown, ClickData>
        {
            public ClickData Data { get { return this.param1; } }
        }

        /// <summary>
        /// ドラッグ時のイベント
        /// </summary>
        public class Drag : Message<Drag, Vector3>
        {
            public Vector3 DeltaPosition { get { return this.param1; } }
        }

        public class ClickData
        {
            private static ClickData cache = new ClickData();
            public static ClickData Get(int buttonId, Vector2 position)
            {
                cache.ButtonId = buttonId;
                cache.Position = position;

                return cache;
            }

            public int ButtonId { get; private set; }

            public Vector2 Position { get; private set; }
        }
    }
}
