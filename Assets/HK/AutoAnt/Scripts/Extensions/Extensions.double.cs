using System.Collections.Generic;
using System.Linq;

namespace HK.AutoAnt.Extensions
{
    /// <summary>
    /// <see cref="double"/>に関する拡張関数
    /// </summary>
    public static partial class Extensions
    {

        /// <summary>
        /// 単位をつけた文字列に変換します。
        /// 12 -> 12
        /// 1234 -> 1.23k
        /// 123456 -> 123k
        /// 12345678 -> 12.3m
        /// 
        /// 単位：k, m, b, t, A, B, C, ... Z, AA, AB, AC, ...
        /// https://blog.naichilab.com/entry/double-unit-string
        /// </summary>
        public static string ToReadableString(this double d, string format)
        {
            //マイナスは扱う気無し
            if (d <= 0) return "0";
            //表示上は整数として見せるので、suffix(kmbtABC等)がつかない場合は少数部を破棄
            if (d <= 1000) return ((int)d).ToString();

            //有効桁数３桁＋指数表記で文字列化
            var s = d.ToString("0.0000E000");

            //有効数字
            float f = float.Parse(s.Substring(0, 6));

            //10の指数
            var e = int.Parse(s.Substring(7, 3));

            //中途半端な指数は数値に掛け合わせておく
            for (var i = 0; i < e % 3; i++)
            {
                f *= 10;
            }

            return f.ToString(format) + LevelToSuffix(e / 3);
        }

        /// <summary>
        /// 10^3ごとにつけるSuffixの一覧
        /// </summary>
        private static string[] _suffixes;

        private static string LevelToSuffix(int level)
        {
            if (_suffixes == null)
            {
                const string AtoZ = " ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                var list = new List<string>();
                list.Add("");
                list.Add("k");
                list.Add("m");
                list.Add("b");
                list.Add("t");
                for (var x = 0; x < AtoZ.Length; x++)
                    for (var y = 1; y < AtoZ.Length; y++)
                    {
                        var str = string.Format("{0}{1}", AtoZ[x], AtoZ[y]).Trim();
                        list.Add(str);
                    }

                _suffixes = list.Take(308).ToArray(); //doubleは指数308まで
            }

            return _suffixes[level];
        }
    }
}
