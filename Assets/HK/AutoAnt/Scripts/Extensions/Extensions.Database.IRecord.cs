using System.Collections.Generic;
using System.Linq;
using HK.AutoAnt.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Extensions
{
    /// <summary>
    /// <see cref="Database.IRecord"/>に関する拡張関数
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// IDからレコードを返す
        /// </summary>
        public static T Get<T>(this IEnumerable<T> self, int id) where T : class, IRecord
        {
            var result = self.FirstOrDefault(r => r.Id == id);
            Assert.IsNotNull(result, $"Id = {id}に対応する{typeof(T)}がありませんでした");

            return result;
        }

        /// <summary>
        /// IDとレベルからレコードを返す
        /// </summary>
        public static T Get<T>(this IEnumerable<T> self, int id, int level) where T : class, IRecord, IRecordLevel
        {
            var result = self.FirstOrDefault(r => r.Id == id && r.Level == level);
            Assert.IsNotNull(result, $"Id = {id}, Level = {level}に対応する{typeof(T)}がありませんでした");

            return result;
        }
    }
}
