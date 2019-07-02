using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Events
{
    /// <summary>
    /// クリックモードへ切り替えをリクエストするイベント
    /// </summary>
    public sealed class RequestClickMode : Message<RequestClickMode>
    {
    }
}
