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
    [CustomEditor(typeof(MasterDataFacilityLevelParameter))]
    public class FacilityLevelParameterEditor : BaseGoogleEditor<MasterDataFacilityLevelParameter>
    {
        private const string WorkSheetName = "FacilityLevelParameter";

        private const string SpreadSheetName = "AutoAntFactory";

        public override bool Load()
        {
            return Load(target as MasterDataFacilityLevelParameter);
        }

        public static bool Load(MasterDataFacilityLevelParameter target)
        {
            var client = new DatabaseClient("", "");
            var error = string.Empty;
            var db = client.GetDatabase(SpreadSheetName, ref error);	
            var table = db.GetTable<FacilityLevelParameterData>(WorkSheetName) ?? db.CreateTable<FacilityLevelParameterData>(WorkSheetName);
            var myDataList = new List<MasterDataFacilityLevelParameter.Record>();
            var all = table.FindAll();

            foreach(var element in all)
            {
                var data = new FacilityLevelParameterData();
                data = Cloner.DeepCopy<FacilityLevelParameterData>(element.Element);
                myDataList.Add(new MasterDataFacilityLevelParameter.Record(data));
            }
                    
            target.Records = myDataList.ToArray();
            
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
            
            return true;
        }
    }
}
