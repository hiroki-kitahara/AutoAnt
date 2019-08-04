using UnityEngine;
using UnityEngine.UI;

namespace HK.AutoAnt.UI.Elements
{
    /// <summary>
    /// 閉じるボタンを制御するクラス
    /// </summary>
    public sealed class CloseButton : MonoBehaviour
    {
        [SerializeField]
        private Button button = null;
        public Button Button => this.button;
    }
}
