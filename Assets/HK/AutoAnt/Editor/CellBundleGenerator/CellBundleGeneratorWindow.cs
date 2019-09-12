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

        private Vector2 cellBundleScrollPosition;

        private float cellSize = 20.0f;

        private static GUIContent cellGUIContent = new GUIContent();

        [MenuItem("AutoAnt/Tool/CellBundleGenerator")]
        private static void CreateWindow()
        {
            var window = EditorWindow.GetWindow<CellBundleGeneratorWindow>();
            window.titleContent = new GUIContent("CellBundleGenerator");
        }

        private void OnEnable()
        {
            var masterData = AssetDatabase.LoadAssetAtPath<MasterData>("Assets/HK/AutoAnt/DataSources/Database/MasterData/MasterData.asset");
            Assert.IsNotNull(masterData, $"{typeof(MasterData).Name}の読み込みに失敗しました");
            this.target = masterData.CellBundle;
            Assert.IsNotNull(this.target, $"{typeof(MasterDataCellBundle).Name}の読み込みに失敗しました");

            this.range = new RectInt();
            foreach (var r in this.target.Records)
            {
                if (this.range.x > r.Rect.x)
                {
                    this.range.x = r.Rect.x;
                }
                if (this.range.y > r.Rect.y)
                {
                    this.range.y = r.Rect.y;
                }
                var width = r.Rect.x + r.Rect.width;
                if (this.range.width < width)
                {
                    this.range.width = width;
                }
                var height = r.Rect.y + r.Rect.height;
                if (this.range.height < height)
                {
                    this.range.height = height;
                }
            }

            if(EditorPrefs.HasKey(EditorPrefsKey.CellSize))
            {
                this.cellSize = EditorPrefs.GetFloat(EditorPrefsKey.CellSize);
            }
        }

        void OnGUI()
        {
            this.DrawSystem();
            this.DrawLine();
            this.DrawCellBundle();
        }

        private void DrawSystem()
        {
            GUILayout.Label("System");
            EditorGUI.indentLevel++;
            EditorGUILayout.ObjectField("Target", this.target, typeof(MasterDataCellBundle), false);
            EditorGUILayout.RectIntField("Range", this.range);

            EditorGUI.BeginChangeCheck();
            this.cellSize = EditorGUILayout.FloatField("CellSize", this.cellSize);
            if(EditorGUI.EndChangeCheck())
            {
                EditorPrefs.SetFloat(EditorPrefsKey.CellSize, this.cellSize);
            }
        }

        private void DrawLine()
        {
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
        }

        private void DrawCellBundle()
        {
            var tempColor = GUI.color;
            this.cellBundleScrollPosition = EditorGUILayout.BeginScrollView(this.cellBundleScrollPosition);

            var width = GUILayout.Width(this.cellSize);
            var height = GUILayout.Height(this.cellSize);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Cell", width);
            GUI.skin.label.alignment = TextAnchor.MiddleCenter;
            for (var x = this.range.x; x <= this.range.width; x++)
            {
                GUILayout.Label(x.ToString(), width);
            }
            EditorGUILayout.EndHorizontal();
            for (var y = this.range.y; y <= this.range.height; y++)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label(y.ToString(), width, height);
                for (var x = this.range.x; x <= this.range.width; x++)
                {
                    GUILayout.Button(cellGUIContent, width, height);
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();
        }

        private static class EditorPrefsKey
        {
            public const string CellSize = "CellBundleGeneratorWindow.CellSize";
        }
    }
}
