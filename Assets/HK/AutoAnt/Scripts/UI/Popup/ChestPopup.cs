using HK.AutoAnt.UI.Elements;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// 貯蔵ポップアップを制御するクラス
    /// </summary>
    public sealed class ChestPopup : TweenPopup
    {
        [SerializeField]
        private TextMeshProUGUI title = null;
        public TextMeshProUGUI Title => this.title;

        [SerializeField]
        private GridList gridList = null;
        public GridList GridList => this.gridList;
    }
}
