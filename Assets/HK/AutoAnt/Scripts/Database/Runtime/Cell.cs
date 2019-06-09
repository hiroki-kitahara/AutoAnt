using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace HK.AutoAnt.Database
{
    ///
    /// !!! Machine generated code !!!
    ///
    /// A class which deriveds ScritableObject class so all its data 
    /// can be serialized onto an asset data file.
    /// 
    [System.Serializable]
    public class Cell : ScriptableObject
    {
        [HideInInspector] [SerializeField] 
        public string SheetName = "";
        
        [HideInInspector] [SerializeField] 
        public string WorksheetName = "";
        
        // Note: initialize in OnEnable() not here.
        public CellData[] Records;
        
        void OnEnable()
        {
            // Important:
            //    It should be checked an initialization of any collection data before it is initialized.
            //    Without this check, the array collection which already has its data get to be null 
            //    because OnEnable is called whenever Unity builds.
            // 
            if (Records == null)
                Records = new CellData[0];
        }

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
    }
}
