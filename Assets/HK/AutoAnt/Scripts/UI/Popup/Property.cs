using System;
using TMPro;
using UnityEngine;

namespace HK.AutoAnt.UI.Elements
{
    /// <summary>
    /// PrefixとValueを表示するUIElement
    /// </summary>
    public sealed class Property : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI prefix = null;
        public TextMeshProUGUI Prefix => this.prefix;

        [SerializeField]
        private TextMeshProUGUI value = null;
        public TextMeshProUGUI Value => this.value;

        private Action<Property> updateAction = null;

        public void Initialize(Action<Property> updateAction)
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
