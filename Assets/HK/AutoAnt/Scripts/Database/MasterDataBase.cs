using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Database
{
    /// <summary>
    /// マスターデータの抽象クラス
    /// </summary>
    public abstract class MasterDataBase<Record> : ScriptableObject, IMasterData<Record> where Record : class
    {
        [SerializeField]
        protected Record[] records = new Record[0];
        public Record[] Records
        {
            get
            {
                return this.records;
            }
            set
            {
                this.records = value;
            }
        }
    }
}
