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
    [CustomEditor(typeof(MasterDataUnlockCellBundle))]
    public class UnlockCellBundleEditor : BaseGoogleEditor<MasterDataUnlockCellBundle>
    {
        private const string WorkSheetName = "UnlockCellBundle";

        private const string SpreadSheetName = "AutoAnt";

        public override bool Load()
        {
            return Load(target as MasterDataUnlockCellBundle);
        }

        public static bool Load(MasterDataUnlockCellBundle target)
        {
            var client = new DatabaseClient("", "");
            var error = string.Empty;
            var db = client.GetDatabase(SpreadSheetName, ref error);	
            var table = db.GetTable<UnlockCellBundleData>(WorkSheetName) ?? db.CreateTable<UnlockCellBundleData>(WorkSheetName);
            var myDataList = new List<MasterDataUnlockCellBundle.Record>();
            var all = table.FindAll();

            foreach(var element in all)
            {
                var data = new UnlockCellBundleData();
                data = Cloner.DeepCopy<UnlockCellBundleData>(element.Element);
                myDataList.Add(new MasterDataUnlockCellBundle.Record(data));
            }
                    
            target.Records = myDataList.ToArray();
            
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
            
            return true;
        }
    }
}
