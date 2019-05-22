using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Database
{
    /// <summary>
    /// 名前を持つレコードのインターフェイス
    /// </summary>
    public interface IRecordName
    {
        /// <summary>
        /// 名前
        /// </summary>
        string Name { get; }
    }
}
