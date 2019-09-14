using System.Linq;
using HK.AutoAnt.Database;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using HK.AutoAnt.Extensions;
using System.Collections.Generic;
using System;

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

        private Color registerCellColor;

        private int currentGroup;

        private string[] groupsString;

        private int[] groupsInt;

        private string[] cellRecordIdString;

        private int[] cellRecordIdInt;

        private Dictionary<Vector2Int, MasterDataCellBundle.Cell> cells = new Dictionary<Vector2Int, MasterDataCellBundle.Cell>();

        private Vector2 cellBundleScrollPosition;

        private bool isVisibleSettings;

        private float cellSize = 20.0f;

        private Color otherGroupCellColor = Color.red;

        private Color chooseableCellColor = Color.gray;

        private static GUIContent cellGUIContent = new GUIContent();

        public enum CellState
        {
            Chooseable,
            OtherGroup,
            CurrentGroup,
        }

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
            if(EditorPrefs.HasKey(EditorPrefsKey.OtherGroupCellColor))
            {
                var otherGroupCellColor = default(Color);
                if(!ColorUtility.TryParseHtmlString(EditorPrefs.GetString(EditorPrefsKey.OtherGroupCellColor), out otherGroupCellColor))
                {
                    Assert.IsTrue(false);
                }
                this.otherGroupCellColor = otherGroupCellColor;
            }
            if(EditorPrefs.HasKey(EditorPrefsKey.ChooseableCellColor))
            {
                var chooseableCellColor = default(Color);
                if (!ColorUtility.TryParseHtmlString(EditorPrefs.GetString(EditorPrefsKey.ChooseableCellColor), out chooseableCellColor))
                {
                    Assert.IsTrue(false);
                }
                this.chooseableCellColor = chooseableCellColor;
            }

            if(EditorPrefs.HasKey(EditorPrefsKey.GetCellColorKey(100000)))
            {
                EditorPrefsKey.SetCellColor(100000, Color.white);
            }
            if (EditorPrefs.HasKey(EditorPrefsKey.GetCellColorKey(100100)))
            {
                EditorPrefsKey.SetCellColor(100100, Color.green);
            }

            this.registerCellColor = EditorPrefsKey.GetCellColor(this.registerCellRecordId);
        }

        void OnGUI()
        {
            this.DrawSystem();
            this.DrawSettings();
            this.DrawLine();
            this.DrawCellBundle();
        }

        private void DrawSystem()
        {
            EditorGUI.indentLevel = 0;
            
            GUILayout.Label("System");
            EditorGUI.indentLevel++;
            EditorGUILayout.ObjectField("Target", this.target, typeof(MasterDataCellBundle), false);

            this.currentGroup = EditorGUILayout.IntPopup("Group", this.currentGroup, this.groupsString, this.groupsInt);

            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginChangeCheck();
            this.registerCellRecordId = EditorGUILayout.IntPopup("CellRecordId", this.registerCellRecordId, this.cellRecordIdString, this.cellRecordIdInt);
            if(EditorGUI.EndChangeCheck())
            {
                this.registerCellColor = EditorPrefsKey.GetCellColor(this.registerCellRecordId);
            }
            EditorGUI.BeginChangeCheck();
            var cellColor = EditorGUILayout.ColorField(this.registerCellColor);
            if(EditorGUI.EndChangeCheck())
            {
                this.registerCellColor = cellColor;
                EditorPrefsKey.SetCellColor(this.registerCellRecordId, this.registerCellColor);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("範囲を広げる");
            if (GUILayout.Button("X"))
            {
                this.range.x--;
            }
            if (GUILayout.Button("Y"))
            {
                this.range.y--;
            }
            if (GUILayout.Button("W"))
            {
                this.range.width++;
            }
            if (GUILayout.Button("H"))
            {
                this.range.height++;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("範囲を狭める");
            if (GUILayout.Button("X"))
            {
                this.NarrowRange(
                    this.range.y,
                    this.range.height,
                    i => new Vector2Int(this.range.x, i),
                    () => this.range.x++
                    );
            }
            if (GUILayout.Button("Y"))
            {
                this.NarrowRange(
                    this.range.x,
                    this.range.width,
                    i => new Vector2Int(i, this.range.y),
                    () => this.range.y++
                    );
            }
            if (GUILayout.Button("W"))
            {
                this.NarrowRange(
                    this.range.y,
                    this.range.height,
                    i => new Vector2Int(this.range.width, i),
                    () => this.range.width--
                    );
            }
            if (GUILayout.Button("H"))
            {
                this.NarrowRange(
                    this.range.x,
                    this.range.width,
                    i => new Vector2Int(i, this.range.height),
                    () => this.range.height--
                    );
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawSettings()
        {
            EditorGUI.indentLevel = 0;

            this.isVisibleSettings = EditorGUILayout.Foldout(this.isVisibleSettings, "Settings");
            if(!this.isVisibleSettings)
            {
                return;
            }

            EditorGUI.indentLevel++;
            EditorGUI.BeginChangeCheck();
            this.cellSize = EditorGUILayout.FloatField("CellSize", this.cellSize);
            if (EditorGUI.EndChangeCheck())
            {
                EditorPrefs.SetFloat(EditorPrefsKey.CellSize, this.cellSize);
            }

            EditorGUI.BeginChangeCheck();
            this.otherGroupCellColor = EditorGUILayout.ColorField("OtherGroupCellColor", this.otherGroupCellColor);
            if (EditorGUI.EndChangeCheck())
            {
                EditorPrefs.SetString(EditorPrefsKey.OtherGroupCellColor, $"#{ColorUtility.ToHtmlStringRGB(this.otherGroupCellColor)}");
            }

            EditorGUI.BeginChangeCheck();
            this.chooseableCellColor = EditorGUILayout.ColorField("ChooseableCellColor", this.chooseableCellColor);
            if (EditorGUI.EndChangeCheck())
            {
                EditorPrefs.SetString(EditorPrefsKey.ChooseableCellColor, $"#{ColorUtility.ToHtmlStringRGB(this.chooseableCellColor)}");
            }
        }

        private void DrawLine()
        {
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
        }

        private void DrawCellBundle()
        {
            EditorGUI.indentLevel = 0;

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
                    var cellState = this.GetCellState(position);
                    switch(cellState)
                    {
                        case CellState.Chooseable:
                            GUI.color = this.chooseableCellColor;
                            break;
                        case CellState.CurrentGroup:
                            GUI.color = EditorPrefsKey.GetCellColor(this.cells[position].Id);
                            break;
                        case CellState.OtherGroup:
                            GUI.color = this.otherGroupCellColor;
                            break;
                        default:
                            Assert.IsTrue(false);
                            break;
                    }
                    if(GUILayout.Button(cellGUIContent, width, height))
                    {
                        this.SetCellData(position, this.registerCellRecordId, this.currentGroup);
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();

            GUI.skin.label.alignment = tempLabelAlignment;
            GUI.color = tempGUIColor;
        }

        private void NarrowRange(int checkMin, int checkMax, Func<int, Vector2Int> positionSelector, Action rangeNarrowAction)
        {
            var confirmed = false;
            for (var i = checkMin; i <= checkMax; i++)
            {
                var position = positionSelector(i);
                if (this.cells.ContainsKey(position) && this.cells[position].Group != -1)
                {
                    if (confirmed || EditorUtility.DisplayDialog("確認", $"{position}には既にデータがありますが本当によろしいですか？", "OK", "CANCEL"))
                    {
                        confirmed = true;
                        this.cells.Remove(position);
                    }
                    else
                    {
                        return;
                    }
                }
            }

            rangeNarrowAction();
        }

        private CellState GetCellState(Vector2Int position)
        {
            if(!this.cells.ContainsKey(position))
            {
                return CellState.Chooseable;
            }

            var cell = this.cells[position];
            if(cell.Group == this.currentGroup)
            {
                return CellState.CurrentGroup;
            }
            if(cell.Group == -1)
            {
                return CellState.Chooseable;
            }

            return CellState.OtherGroup;
        }

        private void SetCellData(Vector2Int position, int cellRecordId, int group)
        {
            if(!this.cells.ContainsKey(position))
            {
                this.cells.Add(position, new MasterDataCellBundle.Cell(cellRecordId, group, position));
                return;
            }
            var cell = this.cells[position];
            if(cell.Group == group)
            {
                cell.Set(-1, -1, cell.Position);
            }
            else if(cell.Group == -1)
            {
                cell.Set(cellRecordId, group, position);
            }
            else if(cell.Group != group)
            {
                Debug.Log($"{position}は他のグループに属しています");
            }
            else
            {
                Assert.IsTrue(false, "未定義の挙動です");
            }
        }

        private void NarrowRange(Rect newRange)
        {

        }

        private static class EditorPrefsKey
        {
            public const string CellSize = "CellBundleGeneratorWindow.CellSize";

            public const string OtherGroupCellColor = "CellBundleGeneratorWindow.OtherGroupCellColor";

            public const string ChooseableCellColor = "CellBundleGeneratorWindow.ChooseableCellColor";

            public static string GetCellColorKey(int cellRecordId)
            {
                return $"CellBundleGeneratorWindow.CellColor.{cellRecordId}";
            }

            public static void SetCellColor(int cellRecordId, Color color)
            {
                EditorPrefs.SetString(GetCellColorKey(cellRecordId), $"#{ColorUtility.ToHtmlStringRGB(color)}");
            }

            public static Color GetCellColor(int cellRecordId)
            {
                var result = default(Color);
                if(!ColorUtility.TryParseHtmlString(EditorPrefs.GetString(GetCellColorKey(cellRecordId)), out result))
                {
                    Assert.IsTrue(false);
                }

                return result;
            }
        }
    }
}
