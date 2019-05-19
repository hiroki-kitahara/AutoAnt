using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers
{
    /// <summary>
    /// <see cref="Cell"/>のイベント生成に関する基本情報
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Cell/EventGenerateSpec")]
    public sealed class CellEventGenerateSpec : ScriptableObject
    {
        [SerializeField]
        private float generateInterval = 1.0f;
        /// <summary>
        /// 生成間隔
        /// </summary>
        public float GenerateInterval => this.generateInterval;
    }
}
