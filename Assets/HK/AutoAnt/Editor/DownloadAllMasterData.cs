using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using HK.AutoAnt.Database;

namespace HK.AutoAnt.Editor
{
    /// <summary>
    /// マスターデータを全てダウンロードするエディタ拡張
    /// </summary>
    public sealed class DownloadAllMasterData
    {
        [MenuItem("AutoAnt/MasterData/Download All")]
        private static void DownloadAll()
        {
            var masterData = AssetDatabase.LoadAssetAtPath<MasterData>("Assets/HK/AutoAnt/DataSources/Database/MasterData/MasterData.asset");
            
        }
    }
}
