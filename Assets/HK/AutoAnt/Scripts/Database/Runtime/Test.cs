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
    public class Test : ScriptableObject
    {
        [HideInInspector] [SerializeField] 
        public string SheetName = "";
        
        [HideInInspector] [SerializeField] 
        public string WorksheetName = "";
        
        // Note: initialize in OnEnable() not here.
        public TestData[] dataArray;
        
        void OnEnable()
        {
            // Important:
            //    It should be checked an initialization of any collection data before it is initialized.
            //    Without this check, the array collection which already has its data get to be null 
            //    because OnEnable is called whenever Unity builds.
            // 
            if (dataArray == null)
                dataArray = new TestData[0];
        }
    }
}
