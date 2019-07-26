using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HK.AutoAnt.Extensions
{
    /// <summary>
    /// <see cref="GameObject"/>に関する拡張関数
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 子オブジェクトのレイヤーも設定する
        /// </summary>
        public static void SetLayerRecursive(this GameObject self, int layer)
        {
            self.layer = layer;
            for (var i = 0; i < self.transform.childCount; i++)
            {
                self.transform.GetChild(i).gameObject.SetLayerRecursive(layer);
            }
        }
    }
}
