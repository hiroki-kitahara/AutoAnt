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
    [CustomEditor(typeof(MasterDataCellEvent))]
    public class CellEventEditor : BaseGoogleEditor<MasterDataCellEvent>
    {
        private const string WorkSheetName = "CellEvent";

        private const string SpreadSheetName = "AutoAnt";

        public override bool Load()
        {        
            var targetData = target as MasterDataCellEvent;
            var client = new DatabaseClient("", "");
            var error = string.Empty;
            var db = client.GetDatabase(SpreadSheetName, ref error);	
            var table = db.GetTable<CellEventData>(WorkSheetName) ?? db.CreateTable<CellEventData>(WorkSheetName);
            var myDataList = new List<MasterDataCellEvent.Record>();
            var all = table.FindAll();

            foreach(var element in all)
            {
                var data = new CellEventData();
                data = Cloner.DeepCopy<CellEventData>(element.Element);
                myDataList.Add(new MasterDataCellEvent.Record(data));
            }
                    
            targetData.Records = myDataList.ToArray();
            
            EditorUtility.SetDirty(targetData);
            AssetDatabase.SaveAssets();
            
            return true;
        }
    }
}
