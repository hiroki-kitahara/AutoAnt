using System;
using HK.AutoAnt.Database;
using HK.AutoAnt.Extensions;
using HK.AutoAnt.Systems;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// スタックされたアイテム
    /// </summary>
    [Serializable]
    public sealed class StackedItem
    {
        /// <summary>
        /// アイテムのID
        /// </summary>
        public int ItemId { get; private set; }

        /// <summary>
        /// アイテムのレコード
        /// </summary>
        public MasterDataItem.Record ItemRecord
        {
            get
            {
                if(this.cachedItemRecord == null)
                {
                    this.cachedItemRecord = GameSystem.Instance.MasterData.Item.Records.Get(this.ItemId);
                }

                return this.cachedItemRecord;
            }
        }
        private MasterDataItem.Record cachedItemRecord = null;

        /// <summary>
        /// アイテムの量
        /// </summary>
        public int Amount { get; set; }

        public StackedItem()
        {
            this.ItemId = 0;
            this.Amount = 0;
        }
        
        public StackedItem(int itemId, int amount)
        {
            this.ItemId = itemId;
            this.Amount = amount;
        }

        /// <summary>
        /// スタック数よりも多く貯蔵しているか返す
        /// </summary>
        public bool IsOverflow()
        {
            return this.Amount > this.ItemRecord.StackNumber;
        }

        /// <summary>
        /// 最大値までスタックしているか返す
        /// </summary>
        public bool IsFull()
        {
            return this.Amount == this.ItemRecord.StackNumber;
        }
    }
}
