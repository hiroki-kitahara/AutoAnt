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
        /// リストに追加可能か返す
        /// </summary>
        bool CanAdd(StackedItem newItem);

        /// <summary>
        /// アイテムを追加
        /// </summary>
        /// <remarks>
        /// 返り値は超過分となっており、<c>null</c>の場合は全て貯蔵出来た状態です
        /// </remarks>
        StackedItem Add(StackedItem newItem);

        /// <summary>
        /// <paramref name="listId"/>に紐づくアイテムを取り出す
        /// </summary>
        void PickOut(int listId);
    }
}
