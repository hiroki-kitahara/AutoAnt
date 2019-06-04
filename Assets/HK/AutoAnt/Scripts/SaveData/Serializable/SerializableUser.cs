using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.SaveData.Serializables
{
    /// <summary>
    /// <see cref="User"/>をシリアライズ可能にするクラス
    /// </summary>
    public sealed class SerializableUser
    {
        public SerializableWallet Wallet { get; set; }
    }
}
