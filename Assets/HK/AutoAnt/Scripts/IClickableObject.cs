using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt
{
    /// <summary>
    /// クリック可能なオブジェクトのインターフェイス
    /// </summary>
    public interface IClickableObject
    {
        void OnClickDown();

        void OnClickUp();
    }
}
