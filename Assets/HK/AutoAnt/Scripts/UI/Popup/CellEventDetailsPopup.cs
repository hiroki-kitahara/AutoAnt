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
    /// セルイベントの詳細を表示するポップアップ
    /// </summary>
    public sealed class CellEventDetailsPopup : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI title = null;

        [SerializeField]
        private ResourceElement resource = null;

        [SerializeField]
        private ResourceElement addPopulation = null;

        [SerializeField]
        private ResourceElement product = null;

        [SerializeField]
        private ResourceElement levelUpCost = null;

        [SerializeField]
        private Button levelUpButton = null;
        public Button LevelUpButton => this.levelUpButton;

        [SerializeField]
        private Button removeButton = null;
        public Button RemoveButton => this.removeButton;

        [SerializeField]
        private StringAsset.Finder cellEventNameAndLevelFormat;

        public void Initialize(CellEvent cellEvent)
        {
        }

        [Serializable]
        public class ResourceElement
        {
            [SerializeField]
            private TextMeshProUGUI prefix;

            [SerializeField]
            private TextMeshProUGUI value;

            public void Apply(string prefix, string value)
            {
                this.prefix.text = prefix;
                this.value.text = value;
            }
        }
    }
}
