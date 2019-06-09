using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using GDataDB;
using GDataDB.Linq;

using UnityQuickSheet;

namespace HK.AutoAnt.Database
{
    ///
    /// !!! Machine generated code !!!
    ///
    [CustomEditor(typeof(Test))]
    public class TestEditor : BaseGoogleEditor<Test>
    {	    
        public override bool Load()
        {        
            Test targetData = target as Test;
            
            var client = new DatabaseClient("", "");
            string error = string.Empty;
            var db = client.GetDatabase(targetData.SheetName, ref error);	
            var table = db.GetTable<TestData>(targetData.WorksheetName) ?? db.CreateTable<TestData>(targetData.WorksheetName);
            
            List<TestData> myDataList = new List<TestData>();
            
            var all = table.FindAll();
            foreach(var elem in all)
            {
                TestData data = new TestData();
                
                data = Cloner.DeepCopy<TestData>(elem.Element);
                myDataList.Add(data);
            }
                    
            targetData.dataArray = myDataList.ToArray();
            
            EditorUtility.SetDirty(targetData);
            AssetDatabase.SaveAssets();
            
            return true;
        }
    }
}
