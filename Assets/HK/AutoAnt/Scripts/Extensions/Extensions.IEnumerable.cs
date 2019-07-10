using System;
using System.Collections.Generic;
using System.Linq;

namespace HK.AutoAnt.Extensions
{
    /// <summary>
    /// <see cref="IEnumerable"/>に関する拡張関数
    /// </summary>
    public static partial class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> self, Action<T> action)
        {
            foreach (var t in self)
            {
                action(t);
            }
        }
    }
}
