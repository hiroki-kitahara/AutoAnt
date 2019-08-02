using HK.AutoAnt.Systems;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Events
{
    /// <summary>
    /// ゲームが開始された際のイベント
    /// </summary>
    public sealed class GameStart : Message<GameStart>
    {
    }
}
