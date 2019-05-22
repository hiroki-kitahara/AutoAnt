using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Database
{
    /// <summary>
    /// マスターデータのインターフェイス
    /// </summary>
    public interface IMasterData<Record> where Record : class, IRecord
    {
        Record[] Records { get; }
    }
}
