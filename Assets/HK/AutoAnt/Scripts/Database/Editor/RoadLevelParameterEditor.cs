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
    [CustomEditor(typeof(MasterDataRoadLevelParameter))]
    public class RoadLevelParameterEditor : BaseGoogleEditor<MasterDataRoadLevelParameter>
    {
        private const string WorkSheetName = "RoadLevelParameter";

        private const string SpreadSheetName = "AutoAnt";

        public override bool Load()
        {        
            var targetData = target as MasterDataRoadLevelParameter;
            var client = new DatabaseClient("", "");
            var error = string.Empty;
            var db = client.GetDatabase(SpreadSheetName, ref error);	
            var table = db.GetTable<RoadLevelParameterData>(WorkSheetName) ?? db.CreateTable<RoadLevelParameterData>(WorkSheetName);
            var myDataList = new List<MasterDataRoadLevelParameter.Record>();
            var all = table.FindAll();

            foreach(var element in all)
            {
                var data = new RoadLevelParameterData();
                data = Cloner.DeepCopy<RoadLevelParameterData>(element.Element);
                myDataList.Add(new MasterDataRoadLevelParameter.Record(data));
            }
                    
            targetData.Records = myDataList.ToArray();
            
            EditorUtility.SetDirty(targetData);
            AssetDatabase.SaveAssets();
            
            return true;
        }
    }
}
