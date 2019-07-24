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
    /// フッターの建設メニューの選択中の建設物のプロパティを制御するクラス
    /// </summary>
    public sealed class FooterSelectedBuildingProperty : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI prefix = null;
        public TextMeshProUGUI Prefix => this.prefix;

        [SerializeField]
        private TextMeshProUGUI value = null;
        public TextMeshProUGUI Value => this.value;

        private Action<FooterSelectedBuildingProperty> updateAction = null;

        public void Initialize(Action<FooterSelectedBuildingProperty> updateAction)
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
