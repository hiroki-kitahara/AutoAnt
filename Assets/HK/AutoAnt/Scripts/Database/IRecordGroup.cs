using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Database
{
    /// <summary>
    /// マスターデータのグループレコードのインターフェイス
    /// </summary>
    public interface IRecordGroup
    {
        /// <summary>
        /// グループ
        /// </summary>
        int Group { get; }
    }
}
