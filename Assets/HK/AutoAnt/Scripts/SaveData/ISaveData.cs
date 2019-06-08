using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.SaveData.Internal
{
    /// <summary>
    /// 1個単位のセーブデータのインターフェイス
    /// </summary>
    public interface ISaveData<T>
    {
        /// <summary>
        /// 保存する
        /// </summary>
        void Save(T value);

        /// <summary>
        /// 読み込む
        /// </summary>
        T Load();

        /// <summary>
        /// データが存在している場合はそのまま読み込み、無い場合は<paramref name="ifNotExistsValue"/>を返す
        /// </summary>
        T Load(T ifNotExistsValue);

        /// <summary>
        /// データが存在するか返す
        /// </summary>
        bool Exists();
    }
}
