using System;
using UnityEngine;

#if UNITY_EDITOR
#endif

namespace HK.AutoAnt.Database
{
    /// <summary>
    /// セルバンドルのマスターデータ
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Database/CellBundle")]
    public sealed class MasterDataCellBundle : MasterDataBase<MasterDataCellBundle.Record>
    {
        [Serializable]
        public class Record : IRecord, IRecordGroup
        {
            [SerializeField]
            private int id = 0;
            public int Id => this.id;

            [SerializeField]
            private int group = 0;
            public int Group => this.group;

            [SerializeField]
            private int cellRecordId = 0;
            public int CellRecordId => this.cellRecordId;

            [SerializeField]
            private Vector2Int position = new Vector2Int();
            public Vector2Int Position => this.position;

#if UNITY_EDITOR
            public Record(SpreadSheetData.CellBundleData data)
            {
                this.id = data.Id;
                this.group = data.Group;
                this.cellRecordId = data.Cellrecordid;
                this.position = new Vector2Int(data.X, data.Y);
            }

            public Record(string data)
            {
                var split = data.Split(',');
                this.id = int.Parse(split[0]);
                this.group = int.Parse(split[1]);
                this.cellRecordId = int.Parse(split[2]);
                this.position = new Vector2Int(int.Parse(split[3]), int.Parse(split[4]));
            }
#endif
        }

#if UNITY_EDITOR
        public void Set(string csv)
        {
            var split = csv.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            this.records = new Record[split.Length - 1];
            for (var i = 1; i < split.Length; i++)
            {
                this.records[i - 1] = new Record(split[i]);
            }
        }
#endif

        /// <summary>
        /// レコードIDと座標のみを持つセル
        /// </summary>
        public class Cell
        {
            public int Id { get; private set; }

            public int Group { get; private set; }

            public Vector2Int Position { get; private set; }

            public Cell(int id, int group, Vector2Int position)
            {
                this.Set(id, group, position);
            }

            public void Set(int id, int group, Vector2Int position)
            {
                this.Id = id;
                this.Group = group;
                this.Position = position;
            }
        }
    }
}
