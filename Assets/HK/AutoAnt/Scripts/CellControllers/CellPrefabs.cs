using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.AutoAnt.Constants;
using System;

namespace HK.AutoAnt.CellControllers
{
    /// <summary>
    /// <see cref="Cell"/>のプレハブ群を保持するクラス
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Cell/Prefabs")]
    public sealed class CellPrefabs : ScriptableObject
    {
        [SerializeField]
        private List<Element> elements = new List<Element>();

        public Cell Get(CellType type)
        {
            var result = this.elements.Find(x => x.Type == type);
            Assert.IsNotNull(result, $"{type}に対応した{typeof(Cell)}のプレハブを取得できませんでした");

            return result.Prefab;
        }

        [Serializable]
        public class Element
        {
            [SerializeField]
            private CellType type = CellType.Grassland;
            public CellType Type => this.type;

            [SerializeField]
            private Cell prefab = null;
            public Cell Prefab => this.prefab;
        }
    }
}
