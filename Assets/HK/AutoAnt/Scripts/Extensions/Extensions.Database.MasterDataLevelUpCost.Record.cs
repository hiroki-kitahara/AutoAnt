using System.Collections.Generic;
using System.Linq;
using HK.AutoAnt.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Extensions
{
    /// <summary>
    /// <see cref="Database.MasterDataLevelUpCost.Record"/>に関する拡張関数
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// IDとレベルからレコードを返す
        /// </summary>
        public static MasterDataLevelUpCost.Record Get(this IEnumerable<MasterDataLevelUpCost.Record> self, int id, int level)
        {
            var result = self.FirstOrDefault(r => r.Id == id && r.Level == level);
            Assert.IsNotNull(result, $"Id = {id}, Level = {level}に対応する{typeof(MasterDataLevelUpCost.Record)}がありませんでした");

            return result;
        }
    }
}
