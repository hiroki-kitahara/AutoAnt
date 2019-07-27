using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// フッターのキャンセルメニューを制御するクラス
    /// </summary>
    public sealed class FooterCancelController : FooterElement
    {
        public override void Open()
        {
            this.gameObject.SetActive(true);
        }
        public override void Close()
        {
            this.gameObject.SetActive(false);
        }
    }
}
