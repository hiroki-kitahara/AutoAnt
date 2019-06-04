using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.SaveData.Serializables
{
    /// <summary>
    /// <see cref="Wallet"/>をシリアライズ可能にするクラス
    /// </summary>
    public sealed class SerializableWallet
    {
        public int Money { get; set; }
    }
}
