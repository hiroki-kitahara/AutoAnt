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
        private GameObject selectBuilding = null;

        void Awake()
        {
            this.ShowRoot();
        }

        public void ShowRoot()
        {
            this.AllHide();
            this.root.SetActive(true);
        }

        public void ShowSelectBuilding()
        {
            this.AllHide();
            this.selectBuilding.SetActive(true);
        }

        private void AllHide()
        {
            this.root.SetActive(false);
            this.selectBuilding.SetActive(false);
        }
    }
}
