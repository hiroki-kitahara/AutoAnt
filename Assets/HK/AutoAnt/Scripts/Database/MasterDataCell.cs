using System;
using System.Collections.Generic;
using HK.AutoAnt.CellControllers;
using HK.AutoAnt.CellControllers.Events;
using HK.AutoAnt.Constants;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Database
{
    /// <summary>
    /// セルのマスターデータ
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Database/Cell")]
    public sealed class MasterDataCell : MasterDataBase<MasterDataCell.Record>
    {
        [SerializeField]
        private ConstantData constants = null;
        public ConstantData Constants => this.constants;

        [Serializable]
        public class ConstantData
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
            private int developCost = 10;
            public int DevelopCost => this.developCost;
        }

        [Serializable]
        public class Record : IRecord
        {
            [SerializeField]
            private int id = 0;
            public int Id => this.id;

            [SerializeField]
            private CellType cellType = CellType.Grassland;
            public CellType CellType => this.cellType;

            [SerializeField]
            private Cell prefab = null;
            public Cell Prefab => this.prefab;

            [SerializeField]
            private List<CellEvent> clickEvents = new List<CellEvent>();
            public List<CellEvent> ClickEvents => this.clickEvents;
        }
    }
}
