using HK.AutoAnt.CellControllers;
using HK.AutoAnt.SaveData.Internal;
using HK.AutoAnt.SaveData.Serializables;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.SaveData
{
    /// <summary>
    /// ユーザー関連のセーブデータ
    /// </summary>
    public static partial class LocalSaveData
    {
        public static readonly ISaveData<SerializableUser> User = new SaveData<SerializableUser>("User");
    }
}
