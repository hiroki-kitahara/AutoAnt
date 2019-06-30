using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Events
{
    /// <summary>
    /// 建設モードへ切り替えをリクエストするイベント
    /// </summary>
    public sealed class RequestBuildingMode : Message<RequestBuildingMode, int>
    {
        /// <summary>
        /// 建設したいセルイベントのレコードID
        /// </summary>
        public int BuildingCellEventRecordId => this.param1;
    }
}
