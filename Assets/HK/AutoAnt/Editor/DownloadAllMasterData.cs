using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using HK.AutoAnt.Database;
using System;
using HK.AutoAnt.Database.SpreadSheetData;

namespace HK.AutoAnt.Editor
{
    /// <summary>
    /// マスターデータを全てダウンロードするエディタ拡張
    /// </summary>
    public sealed class DownloadAllMasterData
    {
        [MenuItem("AutoAnt/MasterData/Download All", false, 1)]
        private static void DownloadAll()
        {
        }

        [MenuItem("AutoAnt/MasterData/Download Cell", false, 12)]
        private static void DownloadCell()
        {
            Download(typeof(MasterDataCell), 1.0f, m => CellEditor.Load(m.Cell));
        }

        private static void Download(Type masterDataType, float progress, Func<MasterData, bool> selector)
        {
            EditorUtility.DisplayProgressBar("マスターデータダウンロード", masterDataType.Name, progress);
            if(!selector(GetMasterData()))
            {
                Debug.LogError($"{masterDataType}のダウンロードに失敗しました");
            }
            EditorUtility.ClearProgressBar();
        }

        private static MasterData GetMasterData()
        {
            return AssetDatabase.LoadAssetAtPath<MasterData>("Assets/HK/AutoAnt/DataSources/Database/MasterData/MasterData.asset");
        }
    }
}
