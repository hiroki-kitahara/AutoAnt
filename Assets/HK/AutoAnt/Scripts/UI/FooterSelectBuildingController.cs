using System.Collections.Generic;
using HK.AutoAnt.Database;
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

        private readonly List<FooterSelectBuildingElement> elements = new List<FooterSelectBuildingElement>();

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
