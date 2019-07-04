using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace HK.AutoAnt.Extensions
{
    /// <summary>
    /// <see cref="StringBuilder"/>に関する拡張関数
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// colorタグ付きの文字列をアペンドする
        /// </summary>
        public static StringBuilder AppendColorCode(this StringBuilder self, Color color, string value)
        {
            return self
                .Append("<color=#")
                .Append(ColorUtility.ToHtmlStringRGBA(color))
                .Append(">")
                .Append(value)
                .Append("</color>");
        }
    }
}
