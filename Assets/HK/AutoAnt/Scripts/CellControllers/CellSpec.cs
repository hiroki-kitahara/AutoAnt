using System.Collections.Generic;
using HK.AutoAnt.CellControllers.ClickEvents;
using HK.AutoAnt.Constants;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers
{
    /// <summary>
    /// <see cref="Cell"/>の基本情報
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Cell/Spec")]
    public sealed class CellSpec : ScriptableObject
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

        [SerializeField]
        private List<CellUnitSpec> unitSpecs = new List<CellUnitSpec>();

        public Cell GetPrefab(CellType type)
        {
            return this.GetUnitSpec(type).Prefab;
        }

        public CellUnitSpec GetUnitSpec(CellType type)
        {
            var result = this.unitSpecs.Find(x => x.Type == type);
            Assert.IsNotNull(result, $"{type}に対応した{typeof(Cell)}の{typeof(CellUnitSpec)}を取得できませんでした");

            return result;
        }
    }
}
