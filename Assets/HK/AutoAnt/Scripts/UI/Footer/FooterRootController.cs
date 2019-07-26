using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// フッターのルート（一番最初のメニュー）を制御するクラス
    /// </summary>
    public sealed class FooterRootController : FooterElement
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
