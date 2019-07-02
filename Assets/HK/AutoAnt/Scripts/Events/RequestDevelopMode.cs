using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Events
{
    /// <summary>
    /// 開拓モードへ切り替えをリクエストするイベント
    /// </summary>
    public sealed class RequestDevelopMode : Message<RequestDevelopMode>
    {
    }
}
