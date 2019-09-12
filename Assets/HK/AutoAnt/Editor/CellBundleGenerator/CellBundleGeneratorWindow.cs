using HK.AutoAnt.Database;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Editor
{
    /// <summary>
    /// <see cref="MasterDataCellBundle"/>を生成するツールウィンドウ
    /// </summary>
    public sealed class CellBundleGeneratorWindow : EditorWindow
    {
        [SerializeField]
        private MasterDataCellBundle target = null;
        
        [MenuItem("AutoAnt/Tool/CellBundleGenerator")]
        private static void CreateWindow()
        {
            var window = EditorWindow.GetWindow<CellBundleGeneratorWindow>();
            window.titleContent = new GUIContent("CellBundleGenerator");
        }

        private void OnEnable()
        {
            Debug.Log("CellBundleGeneratorWindow");
            if(this.target == null)
            {
                var masterData = AssetDatabase.LoadAssetAtPath<MasterData>("Assets/HK/AutoAnt/DataSources/Database/MasterData/MasterData.asset");
                Assert.IsNotNull(masterData, $"{typeof(MasterData).Name}の読み込みに失敗しました");
                this.target = masterData.CellBundle;
                Assert.IsNotNull(this.target, $"{typeof(MasterDataCellBundle).Name}の読み込みに失敗しました");
            }
        }

        void OnGUI()
        {
            GUILayout.Label("System");
            EditorGUI.indentLevel++;
            EditorGUILayout.ObjectField("Target", this.target, typeof(MasterDataCellBundle), false);
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
        }
    }
}
