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

        private void AllHide()
        {
            this.root.SetActive(false);
            this.selectBuilding.gameObject.SetActive(false);
        }
    }
}
