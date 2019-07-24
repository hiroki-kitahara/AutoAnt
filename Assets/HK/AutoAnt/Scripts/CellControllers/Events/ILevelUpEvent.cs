using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers.Events
{
    /// <summary>
    /// レベルアップ可能なセルイベントのインターフェイス
    /// </summary>
    public interface ILevelUpEvent : ICellEvent
    {
        int Id { get; }

        int Level { get; set; }
        
        /// <summary>
        /// レベルアップ可能か返す
        /// </summary>
        bool CanLevelUp();

        /// <summary>
        /// レベルアップを行う
        /// </summary>
        void LevelUp();
    }
}
