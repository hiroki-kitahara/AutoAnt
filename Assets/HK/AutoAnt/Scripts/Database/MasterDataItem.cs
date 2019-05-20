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

        [Serializable]
        public class Element
        {
            [SerializeField]
            private int id;
            public int Id => this.id;

            [SerializeField]
            private StringAsset.Finder name;
            public string Name => this.name.Get;
        }
    }
}
