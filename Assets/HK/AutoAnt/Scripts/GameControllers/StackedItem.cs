using System;

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
        /// アイテムの量
        /// </summary>
        public int Amount { get; private set; }
    }
}
