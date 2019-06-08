using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Systems
{
    /// <summary>
    /// このゲーム全体で利用するシステムを管理するクラス
    /// </summary>
    public class AutoAntSystem : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Setup()
        {
            var prefab = Resources.Load<GameObject>("AutoAntSystems");
            var instance = Instantiate(prefab);
            DontDestroyOnLoad(instance);
        }
    }
}
