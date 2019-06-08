using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.SaveData.Serializables
{
    /// <summary>
    /// <see cref="Cell"/>をシリアライズ可能にするクラス
    /// </summary>
    public sealed class SerializableCell
    {
        public int RecordId { get; set; }
        
        public Vector2Int Position { get; set; }
    }
}
