using UnityEngine;
using UnityEditor;
using System.IO;
using UnityQuickSheet;
using HK.AutoAnt.Database;

///
/// !!! Machine generated code !!!
/// 
public partial class GoogleDataAssetUtility
{
    [MenuItem("Assets/Create/Google/Test")]
    public static void CreateTestAssetFile()
    {
        Test asset = CustomAssetUtility.CreateAsset<Test>();
        asset.SheetName = "AutoAnt";
        asset.WorksheetName = "Test";
        EditorUtility.SetDirty(asset);        
    }
    
}