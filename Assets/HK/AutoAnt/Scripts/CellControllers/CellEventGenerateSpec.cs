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

        [SerializeField]
        private int generateNumber = 1;
        /// <summary>
        /// 一度の処理でイベントを生成する回数
        /// </summary>
        public int GenerateNumber => this.generateNumber;
    }
}
