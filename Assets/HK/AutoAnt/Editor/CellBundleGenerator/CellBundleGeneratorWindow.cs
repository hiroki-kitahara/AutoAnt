using System.Linq;
using HK.AutoAnt.Database;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using HK.AutoAnt.Extensions;
using System.Collections.Generic;

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

        private int registerCellRecordId;

        private int currentGroup;

        private string[] groupsString;

        private int[] groupsInt;

        private string[] cellRecordIdString;

        private int[] cellRecordIdInt;

        private Dictionary<Vector2Int, MasterDataCellBundle.Cell> cells = new Dictionary<Vector2Int, MasterDataCellBundle.Cell>();

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
                var width = r.Rect.x + r.Rect.width - 1;
                if (this.range.width < width)
                {
                    this.range.width = width;
                }
                var height = r.Rect.y + r.Rect.height - 1;
                if (this.range.height < height)
                {
                    this.range.height = height;
                }
            }

            this.groupsInt = this.target.Records
                .Select(x => x.Group)
                .Distinct()
                .ToArray();

            this.groupsString = this.groupsInt
                .Select(x => x.ToString())
                .ToArray();

            var masterDataCell = masterData.Cell;
            this.registerCellRecordId = masterDataCell.Records[0].Id;
            this.cellRecordIdInt = masterDataCell.Records
                .Select(x => x.Id)
                .ToArray();
            this.cellRecordIdString = this.cellRecordIdInt
                .Select(x => x.ToString())
                .ToArray();

            this.cells.Clear();
            foreach(var r in this.target.Records)
            {
                for (var y = r.Rect.y; y < r.Rect.y + r.Rect.height; y++)
                {
                    for (var x = r.Rect.x; x < r.Rect.x + r.Rect.width; x++)
                    {
                        var position = new Vector2Int(x, y);
                        this.cells.Add(position, new MasterDataCellBundle.Cell(r.CellRecordId, r.Group, position));
                    }
                }
            }

            this.currentGroup = this.groupsInt[0];

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

            this.currentGroup = EditorGUILayout.IntPopup("Group", this.currentGroup, this.groupsString, this.groupsInt);

            this.registerCellRecordId = EditorGUILayout.IntPopup("CellRecordId", this.registerCellRecordId, this.cellRecordIdString, this.cellRecordIdInt);

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

            var tempLabelAlignment = GUI.skin.label.alignment;
            var tempGUIColor = GUI.color;

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
                    var position = new Vector2Int(x, y);
                    if(!this.cells.ContainsKey(position))
                    {
                        GUI.color = Color.gray;
                    }
                    else if(this.cells[position].Group == this.currentGroup)
                    {
                        GUI.color = Color.green;
                    }
                    else
                    {
                        GUI.color = Color.red;
                    }
                    GUILayout.Button(cellGUIContent, width, height);
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();

            GUI.skin.label.alignment = tempLabelAlignment;
            GUI.color = tempGUIColor;
        }

        private static class EditorPrefsKey
        {
            public const string CellSize = "CellBundleGeneratorWindow.CellSize";
        }
    }
}
