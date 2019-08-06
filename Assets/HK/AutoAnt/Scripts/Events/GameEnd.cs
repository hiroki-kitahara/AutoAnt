using HK.AutoAnt.Systems;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Events
{
    /// <summary>
    /// ゲームが終了した際のイベント
    /// </summary>
    public sealed class GameEnd : Message<GameEnd>
    {
    }
}
