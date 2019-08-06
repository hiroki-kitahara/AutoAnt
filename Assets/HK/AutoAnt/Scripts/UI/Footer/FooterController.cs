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
        private FooterSelectCellEventController selectBuilding = null;

        [SerializeField]
        private FooterCancelController cancel = null;

        void Awake()
        {
            this.ShowRoot();
        }

        public void ShowRoot()
        {
            this.AllClose();
            this.root.Open();
        }

        public void ShowSelectBuilding(MasterDataCellEvent.Record[] records)
        {
            this.AllClose();
            this.selectBuilding.Open();
            this.selectBuilding.SetData(records);
        }

        public void ShowCancel()
        {
            this.AllClose();
            this.cancel.Open();
        }

        private void AllClose()
        {
            this.root.Close();
            this.selectBuilding.Close();
            this.cancel.Close();
        }
    }
}
