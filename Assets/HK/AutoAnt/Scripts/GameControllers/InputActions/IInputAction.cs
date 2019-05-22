using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// 入力操作によるアクションのインターフェイス
    /// </summary>
    public interface IInputAction<T>
    {
        void Do(T data);
    }
}
