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
    [MenuItem("Assets/Create/Google/Cell")]
    public static void CreateCellAssetFile()
    {
        Cell asset = CustomAssetUtility.CreateAsset<Cell>();
        asset.SheetName = "AutoAnt";
        asset.WorksheetName = "Cell";
        EditorUtility.SetDirty(asset);        
    }
    
}