using HK.AutoAnt.CellControllers.Events;
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

        public void Initialize(CellEvent cellEvent)
        {
        }
    }
}
