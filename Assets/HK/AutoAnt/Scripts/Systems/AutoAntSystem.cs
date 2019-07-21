using HK.AutoAnt.AudioSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Systems
{
    /// <summary>
    /// このゲーム全体で利用するシステムを管理するクラス
    /// </summary>
    public class AutoAntSystem : MonoBehaviour
    {
        [SerializeField]
        private AudioSystem audioSystem = null;
        public static AudioSystem Audio { get; private set; }

        public static readonly LocalNotifiaction LocalNotification = new LocalNotifiaction();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Setup()
        {
            var prefab = Resources.Load<GameObject>("AutoAntSystems");
            Assert.IsNotNull(prefab);

            var instance = Instantiate(prefab);
            DontDestroyOnLoad(instance);

            var system = instance.GetComponent<AutoAntSystem>();
            Assert.IsNotNull(system);

            Audio = system.audioSystem;

            AutoAntSystem.LocalNotification.ClearBadge();
        }

        void OnApplicationFocus(bool status)
        {
            AutoAntSystem.LocalNotification.ClearBadge();
        }
    }
}
