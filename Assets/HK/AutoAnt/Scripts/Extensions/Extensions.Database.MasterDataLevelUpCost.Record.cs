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
        /// <remarks>
        /// <c>null</c>が返る場合はレベルアップ出来ないと判断する
        /// </remarks>
        public static MasterDataLevelUpCost.Record Get(this IEnumerable<MasterDataLevelUpCost.Record> self, int id, int level)
        {
            var result = self.FirstOrDefault(r => r.Id == id && r.Level == level);

            return result;
        }
    }
}
