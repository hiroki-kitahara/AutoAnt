using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Database
{
    /// <summary>
    /// マスターデータのレコードのインターフェイス
    /// </summary>
    public interface IRecord
    {
        /// <summary>
        /// ID
        /// </summary>
        int Id { get; }
    }
}
