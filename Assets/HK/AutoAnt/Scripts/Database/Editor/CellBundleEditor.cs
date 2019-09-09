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
    [CustomEditor(typeof(MasterDataCellBundle))]
    public class CellBundleEditor : BaseGoogleEditor<MasterDataCellBundle>
    {
        private const string WorkSheetName = "CellBundle";

        private const string SpreadSheetName = "AutoAnt";

        public override bool Load()
        {
            return Load(target as MasterDataCellBundle);
        }

        public static bool Load(MasterDataCellBundle target)
        {
            var client = new DatabaseClient("", "");
            var error = string.Empty;
            var db = client.GetDatabase(SpreadSheetName, ref error);	
            var table = db.GetTable<CellBundleData>(WorkSheetName) ?? db.CreateTable<CellBundleData>(WorkSheetName);
            var myDataList = new List<MasterDataCellBundle.Record>();
            var all = table.FindAll();

            foreach(var element in all)
            {
                var data = new CellBundleData();
                data = Cloner.DeepCopy<CellBundleData>(element.Element);
                myDataList.Add(new MasterDataCellBundle.Record(data));
            }
                    
            target.Records = myDataList.ToArray();
            
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
            
            return true;
        }
    }
}
