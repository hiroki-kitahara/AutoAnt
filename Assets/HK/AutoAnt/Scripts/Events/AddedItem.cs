using HK.AutoAnt.Database;
using HK.AutoAnt.UserControllers;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Events
{
    /// <summary>
    /// <see cref="Inventory"/>にアイテムが追加された際のイベント
    /// </summary>
    public sealed class AddedItem : Message<AddedItem, Inventory, MasterDataItem.Element, int>
    {
        /// <summary>
        /// 追加された<see cref="Inventory"/>
        /// </summary>
        public Inventory Inventory => this.param1;

        /// <summary>
        /// 追加されたアイテムのマスターデータ
        /// </summary>
        public MasterDataItem.Element Item => this.param2;

        /// <summary>
        /// 追加された量
        /// </summary>
        public int Amount => this.param3;
    }
}
