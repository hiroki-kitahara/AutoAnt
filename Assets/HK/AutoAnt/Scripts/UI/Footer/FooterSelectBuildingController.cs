using System;
using System.Collections.Generic;
using System.Text;
using HK.AutoAnt.Database;
using HK.AutoAnt.Events;
using HK.AutoAnt.Extensions;
using HK.AutoAnt.Systems;
using HK.Framework.EventSystems;
using HK.Framework.Text;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// フッターメニューの建設メニューを制御するクラス
    /// </summary>
    public sealed class FooterSelectBuildingController : FooterElement
    {
        [SerializeField]
        private RectTransform listRoot = null;

        [SerializeField]
        private FooterSelectBuildingElement elementPrefab = null;

        [SerializeField]
        private GameObject selectedBuildingRoot = null;

        [SerializeField]
        private Transform gimmickParent = null;

        [SerializeField]
        private Transform needItemParent = null;

        [SerializeField]
        private FooterSelectedBuildingProperty propertyPrefab = null;

        [SerializeField]
        private TextMeshProUGUI cellEventName = null;
        public TextMeshProUGUI CellEventName => this.cellEventName;

        [SerializeField]
        private TextMeshProUGUI size = null;
        public TextMeshProUGUI Size => this.size;

        [SerializeField]
        private TextMeshProUGUI money = null;
        public TextMeshProUGUI Money => this.money;

        [SerializeField]
        private StringAsset.Finder sizeFormat = null;
        public StringAsset.Finder SizeFormat => this.sizeFormat;

        [SerializeField]
        private StringAsset.Finder moneyFormat = null;
        public StringAsset.Finder MoneyFormat => this.moneyFormat;

        [SerializeField]
        private StringAsset.Finder needItemFormat = null;
        public StringAsset.Finder NeedItemFormat => this.needItemFormat;

        [SerializeField]
        private Color enoughLevelUpCostColor = Color.white;
        public Color EnoughLevelUpCostColor => this.enoughLevelUpCostColor;

        [SerializeField]
        private Color notEnoughLevelUpCostColor = Color.white;
        public Color NotEnoughLevelUpCostColor => this.notEnoughLevelUpCostColor;

        private GameObject currentGimmick = null;

        private readonly List<FooterSelectBuildingElement> elements = new List<FooterSelectBuildingElement>();

        private readonly List<FooterSelectedBuildingProperty> properties = new List<FooterSelectedBuildingProperty>();

        public override void Open()
        {
            this.gameObject.SetActive(true);
        }

        public override void Close()
        {
            this.gameObject.SetActive(false);
            this.selectedBuildingRoot.SetActive(false);
            this.DestroyGimmick();
        }

        void Awake()
        {
            Broker.Global.Receive<RequestBuildingMode>()
                .Where(_ => this.gameObject.activeInHierarchy)
                .SubscribeWithState(this, (x, _this) =>
                {
                    this.selectedBuildingRoot.SetActive(true);
                    foreach(var p in this.properties)
                    {
                        Destroy(p.gameObject);
                    }
                    this.properties.Clear();
                    x.BuildingCellEventRecord.EventData.AttachFooterSelectCellEvent(_this);
                })
                .AddTo(this);
        }

        public void SetData(MasterDataCellEvent.Record[] records)
        {
            this.ReturnToElements();

            foreach(var r in records)
            {
                var element = this.elementPrefab.Rent(r);
                element.transform.SetParent(this.listRoot, false);
                this.elements.Add(element);
            }
        }

        private void DestroyGimmick()
        {
            if(this.currentGimmick == null)
            {
                return;
            }

            Destroy(this.currentGimmick);
        }

        private void ReturnToElements()
        {
            foreach(var e in this.elements)
            {
                e.ReturnToPool();
            }

            this.elements.Clear();
        }

        public void SetMoney(StringBuilder stringBuilder, double currentMoney, double needMoney)
        {
            stringBuilder.Clear();
            var color = this.GetConditionColor(currentMoney >= needMoney);
            this.Money.text = stringBuilder.AppendColorCode(color, this.MoneyFormat.Format(needMoney.ToReadableString("###"))).ToString();
        }

        public Color GetConditionColor(bool isEnough)
        {
            return isEnough ? this.enoughLevelUpCostColor : this.notEnoughLevelUpCostColor;
        }

        public void CreateGimmick(GameObject gimmickPrefab)
        {
            this.DestroyGimmick();
            this.currentGimmick = Instantiate(gimmickPrefab, this.gimmickParent);
            var t = this.currentGimmick.transform;
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;

            this.currentGimmick.SetLayerRecursive(Layers.Id.UI);
        }

        public FooterSelectedBuildingProperty AddProperty(Action<FooterSelectedBuildingProperty> updateAction)
        {
            var property = Instantiate(this.propertyPrefab, this.needItemParent, false);
            property.Initialize(updateAction);

            this.properties.Add(property);

            return property;
        }
    }
}
