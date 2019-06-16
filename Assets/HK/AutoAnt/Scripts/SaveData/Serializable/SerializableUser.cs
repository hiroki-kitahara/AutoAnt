using HK.AutoAnt.UserControllers;
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

        public Inventory Inventory { get; set; }

        public History History { get; set; }

        public UnlockCellEvent UnlockCellEvent { get; set; }
    }
}
