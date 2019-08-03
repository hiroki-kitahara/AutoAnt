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
    [CustomEditor(typeof(MasterDataLevelUpCost))]
    public class LevelUpCostEditor : BaseGoogleEditor<MasterDataLevelUpCost>
    {
        private const string WorkSheetName = "LevelUpCost";

        private const string SpreadSheetName = "AutoAntFactory";

        public override bool Load()
        {
            return Load(target as MasterDataLevelUpCost);
        }

        public static bool Load(MasterDataLevelUpCost target)
        {
            var client = new DatabaseClient("", "");
            var error = string.Empty;
            var db = client.GetDatabase(SpreadSheetName, ref error);	
            var table = db.GetTable<LevelUpCostData>(WorkSheetName) ?? db.CreateTable<LevelUpCostData>(WorkSheetName);
            var myDataList = new List<MasterDataLevelUpCost.Record>();
            var all = table.FindAll();

            foreach(var element in all)
            {
                var data = new LevelUpCostData();
                data = Cloner.DeepCopy<LevelUpCostData>(element.Element);
                myDataList.Add(new MasterDataLevelUpCost.Record(data));
            }
                    
            target.Records = myDataList.ToArray();
            
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
            
            return true;
        }
    }
}
