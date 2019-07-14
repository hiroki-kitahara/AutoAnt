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
        private FooterRootController root = null;

        [SerializeField]
        private FooterSelectBuildingController selectBuilding = null;

        [SerializeField]
        private FooterCancelController cancel = null;

        void Awake()
        {
            this.ShowRoot();
        }

        public void ShowRoot()
        {
            this.AllHide();
            this.root.Open();
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
            this.cancel.Open();
        }

        private void AllHide()
        {
            this.root.Close();
            this.selectBuilding.gameObject.SetActive(false);
            this.cancel.Close();
        }
    }
}
