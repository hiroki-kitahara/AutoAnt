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
    [CustomEditor(typeof(Cell))]
    public class CellEditor : BaseGoogleEditor<Cell>
    {	    
        public override bool Load()
        {        
            Cell targetData = target as Cell;
            
            var client = new DatabaseClient("", "");
            string error = string.Empty;
            var db = client.GetDatabase(targetData.SheetName, ref error);	
            var table = db.GetTable<CellData>(targetData.WorksheetName) ?? db.CreateTable<CellData>(targetData.WorksheetName);
            
            List<CellData> myDataList = new List<CellData>();
            
            var all = table.FindAll();
            foreach(var elem in all)
            {
                CellData data = new CellData();
                
                data = Cloner.DeepCopy<CellData>(elem.Element);
                myDataList.Add(data);
            }
                    
            targetData.Records = myDataList.ToArray();
            
            EditorUtility.SetDirty(targetData);
            AssetDatabase.SaveAssets();
            
            return true;
        }
    }
}
