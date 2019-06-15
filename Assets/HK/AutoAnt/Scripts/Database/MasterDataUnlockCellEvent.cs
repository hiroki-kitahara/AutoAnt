using System;
using System.Collections.Generic;
using HK.Framework.Text;
using UnityEngine;
using UnityEngine.Assertions;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HK.AutoAnt.Database
{
    /// <summary>
    /// アンロック出来るセルイベントのマスターデータ
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Database/UnlockCellEvent")]
    public sealed class MasterDataUnlockCellEvent : MasterDataBase<MasterDataUnlockCellEvent.Record>
    {
        [Serializable]
        public class Record : IRecord
        {
            [SerializeField]
            private int id = 0;
            public int Id => this.id;

            /// <summary>
            /// アンロック出来るセルイベントのレコードID
            /// </summary>
            [SerializeField]
            private int unlockCellEventRecordId = 0;
            public int UnlockCellEventRecordId => this.unlockCellEventRecordId;

            [SerializeField]
            private NeedCellEvent[] needCellEvents = null;
            public NeedCellEvent[] NeedCellEvents => this.needCellEvents;

#if UNITY_EDITOR
            public Record(SpreadSheetData.UnlockCellEventData data)
            {
                this.id = data.Id;
                this.unlockCellEventRecordId = data.Unlockcelleventrecordid;
                this.needCellEvents = JsonUtility.FromJson<NeedCellEvent.Json>(data.Needcellevent).NeedCellEvent;
            }
#endif

            [Serializable]
            public class NeedCellEvent
            {
                public int CellEventRecordId;
                public int Level;
                public int Number;

                [Serializable]
                public class Json
                {
                    public NeedCellEvent[] NeedCellEvent;
                }
            }
        }
    }
}
