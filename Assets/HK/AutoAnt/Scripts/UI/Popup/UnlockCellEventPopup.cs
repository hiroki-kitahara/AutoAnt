using HK.AutoAnt.CellControllers.Events;
using HK.Framework.Text;
using TMPro;
using UnityEngine;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// アンロックされたセルイベントを表示するポップアップ
    /// </summary>
    public sealed class UnlockCellEventPopup : TweenPopup
    {
        [SerializeField]
        private TextMeshProUGUI message = null;

        [SerializeField]
        private StringAsset.Finder format = null;

        public void Initialize(CellEvent cellEvent)
        {
            this.message.text = this.format.Format(cellEvent.EventName);
        }
    }
}
