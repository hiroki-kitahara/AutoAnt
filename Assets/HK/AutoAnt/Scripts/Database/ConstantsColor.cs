using System;
using HK.AutoAnt.Constants;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Database
{
    /// <summary>
    /// 色に関する定数
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Database/ConstantsColor")]
    public sealed class ConstantsColor : ScriptableObject
    {
        [SerializeField]
        private CellEventColors cellEvent = null;
        public CellEventColors CellEvent => this.cellEvent;

        [Serializable]
        public class CellEventColors
        {
            [SerializeField]
            private Color housing = Color.white;

            [SerializeField]
            private Color farm = Color.white;

            [SerializeField]
            private Color factory = Color.white;

            [SerializeField]
            private Color road = Color.white;

            public Color Get(CellEventCategory category)
            {
                switch(category)
                {
                    case CellEventCategory.Housing:
                        return this.housing;
                    case CellEventCategory.Farm:
                        return this.farm;
                    case CellEventCategory.Factory:
                        return this.factory;
                    case CellEventCategory.Road:
                        return this.road;
                    default:
                        Assert.IsTrue(false, $"{category}は未対応です");
                        return this.housing;
                }
            }
        }
    }
}
