using HK.AutoAnt.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// フッターメニューを制御するクラス
    /// </summary>
    public sealed class FooterController : MonoBehaviour
    {
        [SerializeField]
        private GameObject root = null;

        [SerializeField]
        private FooterSelectBuildingController selectBuilding = null;

        [SerializeField]
        private GameObject cancel;

        void Awake()
        {
            this.ShowRoot();
        }

        public void ShowRoot()
        {
            this.AllHide();
            this.root.SetActive(true);
        }

        public void ShowSelectBuilding(MasterDataCellEvent.Record[] records)
        {
            this.AllHide();
            this.selectBuilding.gameObject.SetActive(true);
            this.selectBuilding.SetData(records);
        }

        public void ShowCancel()
        {
            this.AllHide();
            this.cancel.SetActive(true);
        }

        private void AllHide()
        {
            this.root.SetActive(false);
            this.selectBuilding.gameObject.SetActive(false);
            this.cancel.SetActive(false);
        }
    }
}
