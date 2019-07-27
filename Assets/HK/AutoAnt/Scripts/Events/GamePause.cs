using HK.AutoAnt.Systems;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Events
{
    /// <summary>
    /// ゲームが一時停止した際のイベント
    /// </summary>
    public sealed class GamePause : Message<GamePause>
    {
    }
}
