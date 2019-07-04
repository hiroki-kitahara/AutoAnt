using System;
using HK.AutoAnt.CellControllers.Events;
using HK.Framework.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// セルイベントの詳細のプロパティ部分を表示するポップアップ
    /// </summary>
    public sealed class CellEventDetailsPopupProperty : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI prefix = null;
        public TextMeshProUGUI Prefix => this.prefix;

        [SerializeField]
        private TextMeshProUGUI value = null;
        public TextMeshProUGUI Value => this.value;

        private Action<CellEventDetailsPopupProperty> updateAction = null;

        public void Initialize(Action<CellEventDetailsPopupProperty> updateAction)
        {
            this.updateAction = updateAction;
            this.updateAction(this);
        }

        public void UpdateProperty()
        {
            this.updateAction(this);
        }
    }
}
