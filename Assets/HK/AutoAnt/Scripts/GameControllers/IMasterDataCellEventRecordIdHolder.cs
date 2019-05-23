using UnityEngine;
using UnityEngine.Assertions;
using HK.AutoAnt.Database;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// <see cref="MasterDataCellEvent.Record.Id"/>を保持するインターフェイス
    /// </summary>
    public interface IMasterDataCellEventRecordIdHolder
    {
        int RecordId { get; set; }
    }
}
