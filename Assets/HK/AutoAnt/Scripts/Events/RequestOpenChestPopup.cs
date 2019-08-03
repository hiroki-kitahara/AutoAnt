using HK.AutoAnt.CellControllers.Events;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Events
{
    /// <summary>
    /// 貯蔵ポップアップを開くリクエストイベント
    /// </summary>
    public sealed class RequestOpenChestPopup : Message<RequestOpenChestPopup, Chest>
    {
        public Chest Chest => this.param1;
    }
}
