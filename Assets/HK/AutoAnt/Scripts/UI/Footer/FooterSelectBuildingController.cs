using System.Collections.Generic;
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
        private TextMeshProUGUI cellEventName = null;

        [SerializeField]
        private TextMeshProUGUI size = null;

        [SerializeField]
        private TextMeshProUGUI money = null;

        [SerializeField]
        private StringAsset.Finder sizeFormat = null;

        [SerializeField]
        private StringAsset.Finder moneyFormat = null;

        private GameObject currentGimmick = null;

        private readonly List<FooterSelectBuildingElement> elements = new List<FooterSelectBuildingElement>();

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
                    _this.ApplySelectedBuilding(x.BuildingCellEventRecord);
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

        private void ApplySelectedBuilding(MasterDataCellEvent.Record record)
        {
            this.selectedBuildingRoot.SetActive(true);
            this.CreateGimmick(record);

            this.cellEventName.text = record.EventData.EventNameFromMasterData;
            this.size.text = this.sizeFormat.Format(record.EventData.Size);
            var levelParameter = GameSystem.Instance.MasterData.LevelUpCost.Records.Get(record.Id, 0);
            this.money.text = this.moneyFormat.Format(levelParameter.Cost.Money.ToReadableString("###"));
        }

        private void CreateGimmick(MasterDataCellEvent.Record record)
        {
            this.DestroyGimmick();
            this.currentGimmick = Instantiate(record.EventData.GimmickPrefab, this.gimmickParent);
            var t = this.currentGimmick.transform;
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;

            this.currentGimmick.SetLayerRecursive(Layers.Id.UI);
        }
    }
}
