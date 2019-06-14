using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Database
{
    /// <summary>
    /// セルに関する定数
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Database/ConstantsCell")]
    public sealed class ConstantsCell : ScriptableObject
    {
        [SerializeField]
        private Vector3 scale = new Vector3(1.0f, 0.2f, 1.0f);
        public Vector3 Scale => this.scale;

        [SerializeField]
        private Vector3 effectScale = Vector3.one;
        public Vector3 EffectScale => this.effectScale;

        [SerializeField]
        private float interval = 1.5f;
        public float Interval => this.interval;

        /// <summary>
        /// セル開拓時のSE
        /// </summary>
        [SerializeField]
        private AudioClip developSE = null;
        public AudioClip DevelopSE => this.developSE;

        /// <summary>
        /// 開拓するためのコスト係数
        /// </summary>
        [SerializeField]
        private double developCost = 10;
        public double DevelopCost => this.developCost;
    }
}
