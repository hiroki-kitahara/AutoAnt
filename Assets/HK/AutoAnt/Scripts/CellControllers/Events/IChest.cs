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
        List<StackedItem> Items { get; }
        
        /// <summary>
        /// <paramref name="itemId"/>を追加する
        /// </summary>
        /// <remarks>
        /// 既に<paramref name="itemId"/>を持っている場合はスタックされます
        /// </remarks>
        void Add(int itemId);

        /// <summary>
        /// <paramref name="listId"/>に紐づくアイテムを取り出す
        /// </summary>
        void PickOut(int listId);
    }
}
