using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.SaveData.Serializables
{
    /// <summary>
    /// <see cref="Option"/>をシリアライズ可能にするクラス
    /// </summary>
    public sealed class SerializableOption
    {
        public float BGMVolume { get; set; }

        public float SEVolume { get; set; }
    }
}
