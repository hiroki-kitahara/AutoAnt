using System.Collections.Generic;
using System.Linq;
using HK.AutoAnt.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Extensions
{
    /// <summary>
    /// <see cref="Database.MasterDataCellBundle"/>に関する拡張関数
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// <paramref name="group"/>からセルデータを持つリストを返す
        /// </summary>
        /// <remarks>
        /// 重複した
        /// </remarks>
        public static List<MasterDataCellBundle.Cell> Get(this MasterDataCellBundle self, int group)
        {
            var result = new Dictionary<Vector2Int, MasterDataCellBundle.Cell>();
            var groups = self.Records.GetFromGroup(group);

            foreach(var g in groups)
            {
                var r = g.Rect;
                for (var y = r.y; y < r.y + r.height; y++)
                {
                    for (var x = r.x; x < r.x + r.width; x++)
                    {
                        var position = new Vector2Int(x, y);

                        // 既に存在する場合は上書きする
                        if(result.ContainsKey(position))
                        {
                            var cell = result[position];
                            cell.Id = g.CellRecordId;
                            cell.Position = position;
                        }
                        else
                        {
                            result.Add(position, new MasterDataCellBundle.Cell { Id = g.CellRecordId, Position = position });
                        }
                    }
                }
            }

            return result.Select(x => x.Value).ToList();
        }
    }
}
