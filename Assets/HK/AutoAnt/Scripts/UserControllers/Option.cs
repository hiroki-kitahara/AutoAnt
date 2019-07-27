using System;
using HK.AutoAnt.SaveData.Serializables;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UserControllers
{
    /// <summary>
    /// ユーザーデータのオプションオプションを管理するクラス
    /// </summary>
    [Serializable]
    public sealed class Option
    {
        [SerializeField]
        private FloatReactiveProperty bgmVolume = null;
        public FloatReactiveProperty BGMVolume => this.bgmVolume;

        [SerializeField]
        private FloatReactiveProperty seVolume = null;
        public FloatReactiveProperty SEVolume => this.seVolume;

        public SerializableOption GetSerializable()
        {
            return new SerializableOption()
            {
                BGMVolume = this.bgmVolume.Value,
                SEVolume = this.seVolume.Value
            };
        }

        public void Deserialize(SerializableOption data)
        {
            this.bgmVolume.Value = data.BGMVolume;
            this.seVolume.Value = data.SEVolume;
        }
    }
}
