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
    [CustomEditor(typeof(MasterDataChestParameter))]
    public class ChestParameterEditor : BaseGoogleEditor<MasterDataChestParameter>
    {
        private const string WorkSheetName = "ChestParameter";

        private const string SpreadSheetName = "AutoAntFactory";

        public override bool Load()
        {
            return Load(target as MasterDataChestParameter);
        }

        public static bool Load(MasterDataChestParameter target)
        {
            var client = new DatabaseClient("", "");
            var error = string.Empty;
            var db = client.GetDatabase(SpreadSheetName, ref error);	
            var table = db.GetTable<ChestParameterData>(WorkSheetName) ?? db.CreateTable<ChestParameterData>(WorkSheetName);
            var myDataList = new List<MasterDataChestParameter.Record>();
            var all = table.FindAll();

            foreach(var element in all)
            {
                var data = new ChestParameterData();
                data = Cloner.DeepCopy<ChestParameterData>(element.Element);
                myDataList.Add(new MasterDataChestParameter.Record(data));
            }
                    
            target.Records = myDataList.ToArray();
            
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
            
            return true;
        }
    }
}
