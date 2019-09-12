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

        [SerializeField]
        private RectInt range = new RectInt();

        [MenuItem("AutoAnt/Tool/CellBundleGenerator")]
        private static void CreateWindow()
        {
            var window = EditorWindow.GetWindow<CellBundleGeneratorWindow>();
            window.titleContent = new GUIContent("CellBundleGenerator");
        }

        private void OnEnable()
        {
            if(this.target == null)
            {
                var masterData = AssetDatabase.LoadAssetAtPath<MasterData>("Assets/HK/AutoAnt/DataSources/Database/MasterData/MasterData.asset");
                Assert.IsNotNull(masterData, $"{typeof(MasterData).Name}の読み込みに失敗しました");
                this.target = masterData.CellBundle;
                Assert.IsNotNull(this.target, $"{typeof(MasterDataCellBundle).Name}の読み込みに失敗しました");

                this.range = new RectInt();
                foreach(var r in this.target.Records)
                {
                    if(this.range.x > r.Rect.x)
                    {
                        this.range.x = r.Rect.x;
                    }
                    if(this.range.y > r.Rect.y)
                    {
                        this.range.y = r.Rect.y;
                    }
                    var width = r.Rect.x + r.Rect.width;
                    if(this.range.width < width)
                    {
                        this.range.width = width;
                    }
                    var height = r.Rect.y + r.Rect.height;
                    if(this.range.height < height)
                    {
                        this.range.height = height;
                    }
                }
            }
        }

        void OnGUI()
        {
            GUILayout.Label("System");
            EditorGUI.indentLevel++;
            EditorGUILayout.ObjectField("Target", this.target, typeof(MasterDataCellBundle), false);
            EditorGUILayout.RectIntField("Range", this.range);
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));

            var tempColor = GUI.color;
            var buttonSize = 12.0f;
            for (var y = this.range.y; y <= this.range.height; y++)
            {
                EditorGUILayout.BeginHorizontal();
                for (var x = this.range.x; x <= this.range.width; x++)
                {
                    GUILayout.Button(new GUIContent(), GUILayout.Width(buttonSize), GUILayout.Height(buttonSize));
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
