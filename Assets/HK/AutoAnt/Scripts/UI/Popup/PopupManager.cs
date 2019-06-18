using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// ポップアップを管理するクラス
    /// </summary>
    public sealed class PopupManager : MonoBehaviour
    {
        private static PopupManager instance;

        void Awake()
        {
            Assert.IsNull(instance);
            instance = this;
        }

        void OnDestroy()
        {
            Assert.IsNotNull(instance);
            instance = null;
        }

        public static T Request<T>(T prefab) where T : Popup
        {
            var popup = Instantiate(prefab, instance.transform, false);

            return popup;
        }
    }
}
