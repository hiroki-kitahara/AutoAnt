using System;
using System.Collections.Generic;
using DG.Tweening;
using HK.AutoAnt.CellControllers.Events;
using HK.AutoAnt.Extensions;
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
    public sealed class CellEventDetailsPopup : TweenPopup
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
        private Transform propertyParent = null;

        [SerializeField]
        private Transform levelUpCostParent = null;

        [SerializeField]
        private CellEventDetailsPopupProperty propertyPrefab = null;

        [SerializeField]
        private StringAsset.Finder cellEventNameAndLevelFormat = null;
        public StringAsset.Finder CellEventNameAdnLevelFormat => this.cellEventNameAndLevelFormat;

        [SerializeField]
        private StringAsset.Finder population = null;
        public StringAsset.Finder Population => this.population;

        [SerializeField]
        private StringAsset.Finder popularity = null;
        public StringAsset.Finder Popularity => this.popularity;

        [SerializeField]
        private StringAsset.Finder basePopulation = null;
        public StringAsset.Finder BasePopulation => this.basePopulation;

        [SerializeField]
        private StringAsset.Finder product = null;
        public StringAsset.Finder Product => this.product;

        [SerializeField]
        private StringAsset.Finder productValue = null;
        public StringAsset.Finder ProductValue => this.productValue;

        [SerializeField]
        private StringAsset.Finder money = null;
        public StringAsset.Finder Money => this.money;

        [SerializeField]
        private StringAsset.Finder needItemValue = null;
        public StringAsset.Finder NeedItemValue => this.needItemValue;

        [SerializeField]
        private Color enoughLevelUpCostColor = Color.white;
        public Color EnoughLevelUpCostColor => enoughLevelUpCostColor;

        [SerializeField]
        private Color notEnoughLevelUpCostColor = Color.white;
        public Color NotEnoughLevelUpCostColor => notEnoughLevelUpCostColor;

        private readonly List<CellEventDetailsPopupProperty> properties = new List<CellEventDetailsPopupProperty>();

        private readonly List<CellEventDetailsPopupProperty> levelUpCosts = new List<CellEventDetailsPopupProperty>();

        public IOpenCellEventDetailsPopup SelectCellEvent { get; private set; }

        public void Initialize(IOpenCellEventDetailsPopup cellEvent)
        {
            this.SelectCellEvent = cellEvent;
            this.SelectCellEvent.Attach(this);
            this.UpdateElement();
        }

        public void UpdateElement()
        {
            this.SelectCellEvent.Update(this);
        }

        public void UpdateProperties()
        {
            foreach(var p in this.properties)
            {
                p.UpdateProperty();
            }
        }

        public void ClearLevelUpCosts()
        {
            foreach(var l in this.levelUpCosts)
            {
                Destroy(l.gameObject);
            }
            this.levelUpCosts.Clear();
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
            var property = Instantiate(this.propertyPrefab, this.propertyParent);
            property.Initialize(updateAction);
            this.properties.Add(property);

            return property;
        }

        public CellEventDetailsPopupProperty AddLevelUpCost(Action<CellEventDetailsPopupProperty> updateAction)
        {
            var property = Instantiate(this.propertyPrefab, this.levelUpCostParent);
            property.Initialize(updateAction);
            this.levelUpCosts.Add(property);

            return property;
        }
    }
}
