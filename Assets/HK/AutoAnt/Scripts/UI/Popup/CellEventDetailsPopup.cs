using System;
using System.Collections.Generic;
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
        private Button levelUpButton = null;
        public Button LevelUpButton => this.levelUpButton;

        [SerializeField]
        private Button removeButton = null;
        public Button RemoveButton => this.removeButton;

        [SerializeField]
        private Button closeButton = null;
        public Button CloseButton => this.closeButton;

        [SerializeField]
        private Transform propertyParent;

        [SerializeField]
        private Transform levelUpCostParent;

        [SerializeField]
        private CellEventDetailsPopupProperty propertyPrefab;

        [SerializeField]
        private StringAsset.Finder cellEventNameAndLevelFormat;
        public StringAsset.Finder CellEventNameAdnLevelFormat => this.cellEventNameAndLevelFormat;

        [SerializeField]
        private StringAsset.Finder population;
        public StringAsset.Finder Population => this.population;

        [SerializeField]
        private StringAsset.Finder popularity;
        public StringAsset.Finder Popularity => this.popularity;

        [SerializeField]
        private StringAsset.Finder basePopulation;
        public StringAsset.Finder BasePopulation => this.basePopulation;

        [SerializeField]
        private StringAsset.Finder product;
        public StringAsset.Finder Product => this.product;

        [SerializeField]
        private StringAsset.Finder productValue;
        public StringAsset.Finder ProductValue => this.productValue;

        [SerializeField]
        private StringAsset.Finder money;
        public StringAsset.Finder Money => this.money;

        [SerializeField]
        private StringAsset.Finder needItemValue;
        public StringAsset.Finder NeedItemValue => this.needItemValue;

        private readonly List<CellEventDetailsPopupProperty> properties = new List<CellEventDetailsPopupProperty>();

        public CellEvent SelectCellEvent { get; private set; }

        public void Initialize(CellEvent cellEvent)
        {
            this.SelectCellEvent = cellEvent;
            this.SelectCellEvent.AttachDetailsPopup(this);
            this.UpdateProperties();
        }

        public void UpdateProperties()
        {
            this.SelectCellEvent.UpdateDetailsPopup(this);
        }

        public void SetActiveLevelUpButton(bool isActive)
        {
            this.LevelUpButton.gameObject.SetActive(isActive);
        }

        public void ApplyTitle(string name, int level)
        {
            this.title.text = this.cellEventNameAndLevelFormat.Format(name, level);
        }

        public void ApplyTitle(string name)
        {
            this.title.text = name;
        }

        public CellEventDetailsPopupProperty AddProperty(Action<CellEventDetailsPopupProperty> updateAction)
        {
            return this.InternalAddProperty(updateAction, this.propertyParent);
        }

        public CellEventDetailsPopupProperty AddLevelUpCost(Action<CellEventDetailsPopupProperty> updateAction)
        {
            return this.InternalAddProperty(updateAction, this.levelUpCostParent);
        }

        private CellEventDetailsPopupProperty InternalAddProperty(Action<CellEventDetailsPopupProperty> updateAction, Transform parent)
        {
            var property = Instantiate(this.propertyPrefab, parent);
            property.Initialize(updateAction);
            this.properties.Add(property);

            return property;
        }
    }
}
