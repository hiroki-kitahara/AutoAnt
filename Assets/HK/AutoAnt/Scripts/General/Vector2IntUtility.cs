using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt
{
    /// <summary>
    /// <see cref="Vector2Int"/>に関するユーティリティクラス
    /// </summary>
    public static class Vector2IntUtility
    {
        /// <summary>
        /// <paramref name="origin"/>を原点とした範囲の座標を評価する
        /// </summary>
        /// <remarks>
        /// <paramref name="range"/>が<c>1</c>の場合は以下の範囲を評価する
        /// o = origin
        /// x = range
        /// xxx
        /// xox
        /// xxx
        /// <paramref name="range"/>が<c>2</c>の場合は以下の範囲を評価する
        /// o = origin
        /// x = range
        /// xxxxx
        /// xxxxx
        /// xxoxx
        /// xxxxx
        /// xxxxx
        /// </remarks>
        public static List<Vector2Int> GetRange(Vector2Int origin, int range, Func<Vector2Int, bool> selector)
        {
            Assert.IsTrue(range > 0);

            var result = new List<Vector2Int>();

            for (var y = -range; y <= range; y++)
            {
                for (var x = -range; x <= range; x++)
                {
                    var position = origin + new Vector2Int(x, y);
                    if (!selector(position))
                    {
                        continue;
                    }

                    result.Add(position);
                }
            }

            return result;
        }

        /// <summary>
        /// <paramref name="origin"/>を原点とした範囲の座標を評価する
        /// </summary>
        /// <remarks>
        /// <paramref name="size"/>が<c>(2, 2)</c>の場合は以下の範囲を評価する
        /// o = origin
        /// x = range
        /// xx
        /// ox
        /// </remarks>
        public static List<Vector2Int> GetRange(Vector2Int origin, Vector2Int size, Func<Vector2Int, bool> selector)
        {
            Assert.IsTrue(size.x >= 0);
            Assert.IsTrue(size.y >= 0);

            var result = new List<Vector2Int>();

            for (var y = 0; y < size.y; y++)
            {
                for (var x = 0; x < size.x; x++)
                {
                    var position = origin + new Vector2Int(x, y);
                    if (!selector(position))
                    {
                        continue;
                    }

                    result.Add(position);
                }
            }

            return result;
        }
    }
}
