using System.Collections.Generic;
using HK.AutoAnt.Database;
using HK.AutoAnt.Events;
using HK.AutoAnt.Extensions;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// フッターメニューの建設メニューを制御するクラス
    /// </summary>
    public sealed class FooterSelectBuildingController : MonoBehaviour
    {
        [SerializeField]
        private RectTransform listRoot = null;

        [SerializeField]
        private FooterSelectBuildingElement elementPrefab = null;

        [SerializeField]
        private Transform gimmickParent = null;

        private GameObject currentGimmick = null;

        private readonly List<FooterSelectBuildingElement> elements = new List<FooterSelectBuildingElement>();

        void Awake()
        {
            Broker.Global.Receive<RequestBuildingMode>()
                .Where(_ => this.gameObject.activeInHierarchy)
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.DestroyGimmick();
                    _this.currentGimmick = Instantiate(x.BuildingCellEventRecord.EventData.GimmickPrefab, _this.gimmickParent);
                    var t = _this.currentGimmick.transform;
                    t.localPosition = Vector3.zero;
                    t.localRotation = Quaternion.identity;
                    t.localScale = Vector3.one;
                    
                    _this.currentGimmick.SetLayerRecursive(Layers.Id.UI);
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
    }
}
