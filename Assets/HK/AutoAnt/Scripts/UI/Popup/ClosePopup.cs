using UnityEngine;
using UnityEngine.UI;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// 閉じるポップアップを制御するクラス
    /// </summary>
    public sealed class ClosePopup : TweenPopup
    {
        [SerializeField]
        private Button button = null;
        public Button Button => this.button;
    }
}
