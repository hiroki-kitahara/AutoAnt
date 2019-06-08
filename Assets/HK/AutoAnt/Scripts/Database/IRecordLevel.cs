using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Database
{
    /// <summary>
    /// レベルを持つマスターデータのレコードのインターフェイス
    /// </summary>
    public interface IRecordLevel
    {
        /// <summary>
        /// レベル
        /// </summary>
        int Level { get; }
    }
}
