using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Database
{
    /// <summary>
    /// マスターデータの抽象クラス
    /// </summary>
    public abstract class MasterDataBase<Record> : IMasterData<Record> where Record : class, IRecord
    {
        [SerializeField]
        protected Record[] records = new Record[0];
        public Record[] Records => this.records;
    }
}
