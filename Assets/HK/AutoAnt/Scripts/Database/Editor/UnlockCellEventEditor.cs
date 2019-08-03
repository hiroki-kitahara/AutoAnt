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
    [CustomEditor(typeof(MasterDataUnlockCellEvent))]
    public class UnlockCellEventEditor : BaseGoogleEditor<MasterDataUnlockCellEvent>
    {
        private const string WorkSheetName = "UnlockCellEvent";

        private const string SpreadSheetName = "AutoAntFactory";

        public override bool Load()
        {
            return Load(target as MasterDataUnlockCellEvent);
        }

        public static bool Load(MasterDataUnlockCellEvent target)
        {
            var client = new DatabaseClient("", "");
            var error = string.Empty;
            var db = client.GetDatabase(SpreadSheetName, ref error);	
            var table = db.GetTable<UnlockCellEventData>(WorkSheetName) ?? db.CreateTable<UnlockCellEventData>(WorkSheetName);
            var myDataList = new List<MasterDataUnlockCellEvent.Record>();
            var all = table.FindAll();

            foreach(var element in all)
            {
                var data = new UnlockCellEventData();
                data = Cloner.DeepCopy<UnlockCellEventData>(element.Element);
                myDataList.Add(new MasterDataUnlockCellEvent.Record(data));
            }
                    
            target.Records = myDataList.ToArray();
            
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
            
            return true;
        }
    }
}
