using UnityEngine;
using UnityEngine.Assertions;
using HK.AutoAnt.Database;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// <see cref="MasterDataCell.Record.Id"/>を保持するインターフェイス
    /// </summary>
    public interface IMasterDataCellRecordIdHolder
    {
        int Id { get; set; }
    }
}
