using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using GDataDB;
using GDataDB.Linq;

using UnityQuickSheet;

namespace HK.AutoAnt.Database.SpreadSheetData
{
    ///
    /// !!! Machine generated code !!!
    ///
    [CustomEditor(typeof(MasterDataCell))]
    public class CellEditor : BaseGoogleEditor<MasterDataCell>
    {
        private const string WorkSheetName = "Cell";

        private const string SpreadSheetName = "AutoAnt";

        public override bool Load()
        {        
            var targetData = target as MasterDataCell;
            var client = new DatabaseClient("", "");
            var error = string.Empty;
            var db = client.GetDatabase(SpreadSheetName, ref error);	
            var table = db.GetTable<CellData>(WorkSheetName) ?? db.CreateTable<CellData>(WorkSheetName);
            var myDataList = new List<MasterDataCell.Record>();
            var all = table.FindAll();

            foreach(var element in all)
            {
                var data = new CellData();
                data = Cloner.DeepCopy<CellData>(element.Element);
                myDataList.Add(new MasterDataCell.Record(data));
            }
                    
            targetData.Records = myDataList.ToArray();
            
            EditorUtility.SetDirty(targetData);
            AssetDatabase.SaveAssets();
            
            return true;
        }
    }
}
