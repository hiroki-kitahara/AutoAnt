using System;
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
        public float BGMVolume = 0.5f;

        public float SEVolume = 0.5f;
    }
}
