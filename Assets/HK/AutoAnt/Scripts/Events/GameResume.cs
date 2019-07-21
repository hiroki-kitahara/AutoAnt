using HK.AutoAnt.Systems;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Events
{
    /// <summary>
    /// ゲームが復帰した際のイベント
    /// </summary>
    public sealed class GameResume : Message<GameResume>
    {
    }
}
