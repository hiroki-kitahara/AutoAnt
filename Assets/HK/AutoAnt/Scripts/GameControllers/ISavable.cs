using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// セーブデータとして扱えるインターフェイス
    /// </summary>
    public interface ISavable
    {
        /// <summary>
        /// セーブデータがある場合は読み込み、ない場合は通常通り初期化する
        /// </summary>
        void Initialize();

        /// <summary>
        /// セーブする
        /// </summary>
        void Save();
    }
}
