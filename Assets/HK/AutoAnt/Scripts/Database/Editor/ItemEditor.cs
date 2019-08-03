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
    [CustomEditor(typeof(MasterDataItem))]
    public class ItemEditor : BaseGoogleEditor<MasterDataItem>
    {
        private const string WorkSheetName = "Item";

        private const string SpreadSheetName = "AutoAntFactory";

        public override bool Load()
        {
            return Load(target as MasterDataItem);
        }

        public static bool Load(MasterDataItem target)
        {
            var client = new DatabaseClient("", "");
            var error = string.Empty;
            var db = client.GetDatabase(SpreadSheetName, ref error);	
            var table = db.GetTable<ItemData>(WorkSheetName) ?? db.CreateTable<ItemData>(WorkSheetName);
            var myDataList = new List<MasterDataItem.Record>();
            var all = table.FindAll();

            foreach(var element in all)
            {
                var data = new ItemData();
                data = Cloner.DeepCopy<ItemData>(element.Element);
                myDataList.Add(new MasterDataItem.Record(data));
            }
                    
            target.Records = myDataList.ToArray();
            
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
            
            return true;
        }
    }
}
