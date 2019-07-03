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
    public sealed class CellEventDetailsPopup : Popup
    {
        [SerializeField]
        private TextMeshProUGUI title = null;
        public TextMeshProUGUI Title => this.title;

        [SerializeField]
        private ResourceElement resource = null;
        public ResourceElement Resource => this.resource;

        [SerializeField]
        private ResourceElement addPopulation = null;
        public ResourceElement AddPopulation => this.addPopulation;

        [SerializeField]
        private ResourceElement product = null;
        public ResourceElement Product => this.product;

        [SerializeField]
        private ResourceElement levelUpCost = null;
        public ResourceElement LevelUpCost => this.levelUpCost;

        [SerializeField]
        private Button levelUpButton = null;
        public Button LevelUpButton => this.levelUpButton;

        [SerializeField]
        private Button removeButton = null;
        public Button RemoveButton => this.removeButton;

        [SerializeField]
        private Button closeButton = null;
        public Button CloseButton => this.closeButton;

        [SerializeField]
        private StringAsset.Finder cellEventNameAndLevelFormat;
        public StringAsset.Finder CellEventNameAdnLevelFormat => this.cellEventNameAndLevelFormat;

        public CellEvent SelectCellEvent { get; private set; }

        public void Initialize(CellEvent cellEvent)
        {
            this.SelectCellEvent = cellEvent;
            this.ApplyTitle(cellEvent);

            // TODO
            this.resource.SetActive(false);
            this.addPopulation.SetActive(false);
            this.product.SetActive(false);
            this.levelUpCost.SetActive(false);
        }

        public void UpdateProperties()
        {
            this.ApplyTitle(this.SelectCellEvent);
        }

        public void SetActiveLevelUpButton(bool isActive)
        {
            this.LevelUpButton.gameObject.SetActive(isActive);
        }

        private void ApplyTitle(CellEvent cellEvent)
        {
            if(cellEvent is ILevelUpEvent)
            {
                var levelUpEvent = cellEvent as ILevelUpEvent;
                this.title.text = this.cellEventNameAndLevelFormat.Format(cellEvent.EventName, levelUpEvent.Level);
            }
            else
            {
                this.title.text = cellEvent.EventName;
            }
        }

        [Serializable]
        public class ResourceElement
        {
            [SerializeField]
            private GameObject root;

            [SerializeField]
            private TextMeshProUGUI prefix;

            [SerializeField]
            private TextMeshProUGUI value;

            public void SetActive(bool isActive)
            {
                this.root.SetActive(isActive);
            }

            public void Apply(string prefix, string value)
            {
                this.prefix.text = prefix;
                this.value.text = value;
            }
        }
    }
}
