using System.Collections.Generic;
using System.Linq;
using HK.AutoAnt.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Extensions
{
    /// <summary>
    /// <see cref="Database.IRecordName"/>に関する拡張関数
    /// </summary>
    public static partial class Extensions
    {
        public static T GetById<T>(this IEnumerable<T> self, string name) where T : class, IRecordName
        {
            var result = self.FirstOrDefault(r => r.Name == name);
            Assert.IsNotNull(result, $"Name = {name}に対応する{typeof(T)}がありませんでした");

            return result;
        }
    }
}
