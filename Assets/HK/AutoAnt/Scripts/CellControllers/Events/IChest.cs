using System.Collections.Generic;
using HK.AutoAnt.GameControllers;

namespace HK.AutoAnt.CellControllers.Events
{
    /// <summary>
    /// 貯蔵可能なセルイベントのインターフェイス
    /// </summary>
    public interface IChest : ICellEvent
    {
        /// <summary>
        /// 貯蔵しているアイテム
        /// </summary>
        StackedItem[] Items { get; }
        
        /// <summary>
        /// アイテムを追加
        /// </summary>
        void Add(StackedItem stackedItem);

        /// <summary>
        /// <paramref name="listId"/>に紐づくアイテムを取り出す
        /// </summary>
        void PickOut(int listId);
    }
}
