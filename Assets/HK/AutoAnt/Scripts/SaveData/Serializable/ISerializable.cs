using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt
{
    /// <summary>
    /// シリアライズ可能なインターフェイス
    /// </summary>
    public interface ISerializable<T>
    {
        /// <summary>
        /// シリアライズする
        /// </summary>
        T Serialize();

        /// <summary>
        /// デシリアライズする
        /// </summary>
        void Deserialize(T target);
    }
}
