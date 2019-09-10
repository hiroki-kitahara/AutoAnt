using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using HK.AutoAnt.Database;
using System;
using HK.AutoAnt.Database.SpreadSheetData;
using System.Collections.Generic;

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
            var downloader = new Action<float>[]
            {
                (p) => Download(typeof(MasterDataItem), p, m => ItemEditor.Load(m.Item), false),
                (p) => Download(typeof(MasterDataCell), p, m => CellEditor.Load(m.Cell), false),
                (p) => Download(typeof(MasterDataCellEvent), p, m => CellEventEditor.Load(m.CellEvent), false),
                (p) => Download(typeof(MasterDataLevelUpCost), p, m => LevelUpCostEditor.Load(m.LevelUpCost), false),
                (p) => Download(typeof(MasterDataHousingLevelParameter), p, m => HousingLevelParameterEditor.Load(m.HousingLevelParameter), false),
                (p) => Download(typeof(MasterDataFacilityLevelParameter), p, m => FacilityLevelParameterEditor.Load(m.FacilityLevelParameter), false),
                (p) => Download(typeof(MasterDataRoadLevelParameter), p, m => RoadLevelParameterEditor.Load(m.RoadLevelParameter), false),
                (p) => Download(typeof(MasterDataUnlockCellEvent), p, m => UnlockCellEventEditor.Load(m.UnlockCellEvent), false),
                (p) => Download(typeof(MasterDataCellBundle), p, m => CellBundleEditor.Load(m.CellBundle), false),
                (p) => Download(typeof(MasterDataUnlockCellBundle), p, m => UnlockCellBundleEditor.Load(m.UnlockCellBundle), false),
            };

            for (var i = 0; i < downloader.Length; i++)
            {
                var progress = (float)i / downloader.Length;
                downloader[i](progress);
            }

            EditorUtility.ClearProgressBar();
        }

        [MenuItem("AutoAnt/MasterData/Download Item", false, 12)]
        private static void DownloadItem()
        {
            Download(typeof(MasterDataItem), 1.0f, m => ItemEditor.Load(m.Item));
        }

        [MenuItem("AutoAnt/MasterData/Download Cell", false, 13)]
        private static void DownloadCell()
        {
            Download(typeof(MasterDataCell), 1.0f, m => CellEditor.Load(m.Cell));
        }

        [MenuItem("AutoAnt/MasterData/Download CellEvent", false, 14)]
        private static void DownloadCellEvent()
        {
            Download(typeof(MasterDataCellEvent), 1.0f, m => CellEventEditor.Load(m.CellEvent));
        }

        [MenuItem("AutoAnt/MasterData/Download LevelUpCost", false, 15)]
        private static void DownloadLevelUpCost()
        {
            Download(typeof(MasterDataLevelUpCost), 1.0f, m => LevelUpCostEditor.Load(m.LevelUpCost));
        }

        [MenuItem("AutoAnt/MasterData/Download HousingLevelParameter", false, 16)]
        private static void DownloadHousingLevelParameter()
        {
            Download(typeof(MasterDataHousingLevelParameter), 1.0f, m => HousingLevelParameterEditor.Load(m.HousingLevelParameter));
        }

        [MenuItem("AutoAnt/MasterData/Download FacilityLevelParameter", false, 17)]
        private static void DownloadFacilityLevelParameter()
        {
            Download(typeof(MasterDataFacilityLevelParameter), 1.0f, m => FacilityLevelParameterEditor.Load(m.FacilityLevelParameter));
        }

        [MenuItem("AutoAnt/MasterData/Download RoadLevelParameter", false, 18)]
        private static void DownloadRoadLevelParameter()
        {
            Download(typeof(MasterDataRoadLevelParameter), 1.0f, m => RoadLevelParameterEditor.Load(m.RoadLevelParameter));
        }

        [MenuItem("AutoAnt/MasterData/Download UnlockCellEvent", false, 19)]
        private static void DownloadUnlockCellEvent()
        {
            Download(typeof(MasterDataUnlockCellEvent), 1.0f, m => UnlockCellEventEditor.Load(m.UnlockCellEvent));
        }

        [MenuItem("AutoAnt/MasterData/Download CellBundle", false, 20)]
        private static void DownloadCellBundle()
        {
            Download(typeof(MasterDataCellBundle), 1.0f, m => CellBundleEditor.Load(m.CellBundle));
        }

        [MenuItem("AutoAnt/MasterData/Download UnlockCellBundle", false, 21)]
        private static void DownloadUnlockCellBundle()
        {
            Download(typeof(MasterDataUnlockCellBundle), 1.0f, m => UnlockCellBundleEditor.Load(m.UnlockCellBundle));
        }

        private static void Download(Type masterDataType, float progress, Func<MasterData, bool> selector, bool clearProgressBar = true)
        {
            EditorUtility.DisplayProgressBar("マスターデータダウンロード", masterDataType.Name, progress);
            if(!selector(GetMasterData()))
            {
                Debug.LogError($"{masterDataType}のダウンロードに失敗しました");
            }
            if(clearProgressBar)
            {
                EditorUtility.ClearProgressBar();
            }
        }

        private static MasterData GetMasterData()
        {
            return AssetDatabase.LoadAssetAtPath<MasterData>("Assets/HK/AutoAnt/DataSources/Database/MasterData/MasterData.asset");
        }
    }
}
