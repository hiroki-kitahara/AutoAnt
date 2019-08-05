using HK.AutoAnt.CellControllers.Events;
using HK.AutoAnt.Database;
using HK.AutoAnt.UserControllers;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Events
{
    /// <summary>
    /// <see cref="IChest"/>に貯蔵されているアイテムに更新が入った際のイベント
    /// </summary>
    public sealed class UpdatedStackedItemInChest : Message<UpdatedStackedItemInChest, IChest, int>
    {
        public IChest Chest => this.param1;

        public int ItemsIndex => this.param2;
    }
}
