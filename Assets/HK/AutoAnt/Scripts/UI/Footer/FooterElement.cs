using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// フッターの要素を制御する抽象クラス
    /// </summary>
    public abstract class FooterElement : MonoBehaviour, IFooterElement
    {
        public abstract void Open();

        public abstract void Close();
    }
}
