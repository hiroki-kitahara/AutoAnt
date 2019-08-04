using System;
using UnityEngine;

#if UNITY_EDITOR
#endif

namespace HK.AutoAnt.Database
{
    /// <summary>
    /// チェストのマスターデータ
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Database/ChestParameter")]
    public sealed class MasterDataChestParameter : MasterDataBase<MasterDataChestParameter.Record>
    {
        [Serializable]
        public class Record : IRecord
        {
            [SerializeField]
            private int id = 0;
            public int Id => this.id;

            /// <summary>
            /// アイテムを貯蔵出来る容量
            /// </summary>
            [SerializeField]
            private int capacity = 1;
            public int Capacity => this.capacity;

#if UNITY_EDITOR
#endif
        }
    }
}
