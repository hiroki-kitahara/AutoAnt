using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt
{
    /// <summary>
    /// カメラマン
    /// </summary>
    public sealed class Cameraman : MonoBehaviour
    {
        [SerializeField]
        private Transform root;
        public Transform Root => this.root;

        [SerializeField]
        private Transform pivot;

        [SerializeField]
        private Transform rig;

        [SerializeField]
        private Transform distance;

        [SerializeField]
        private Camera controlledCamera;
        public Camera Camera => this.controlledCamera;

        public static Cameraman Instance { get; private set; }

        void Awake()
        {
            Assert.IsNull(Instance);
            Instance = this;
        }

        void OnDestroy()
        {
            Assert.IsNotNull(Instance);
            Instance = null;
        }
    }
}
