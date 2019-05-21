using System;
using System.Collections.Generic;
using HK.Framework.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Database
{
    /// <summary>
    /// アイテムのマスターデータ
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Database/Item")]
    public sealed class MasterDataItem : ScriptableObject
    {
        [SerializeField]
        private List<Element> elements = new List<Element>();

        public Element GetByName(string name)
        {
            var result = this.elements.Find(e => e.Name == name);
            Assert.IsNotNull(result, $"{name}に対応するアイテムがありませんでした");

            return result;
        }

        [Serializable]
        public class Element
        {
            [SerializeField]
            private int id = 0;
            public int Id => this.id;

            [SerializeField]
            private StringAsset.Finder name = null;
            public string Name => this.name.Get;
        }
    }
}
