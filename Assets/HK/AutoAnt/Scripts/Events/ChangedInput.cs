using HK.Framework.EventSystems;
using HK.AutoAnt.Constants;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Events
{
    /// <summary>
    /// InputModeが切り替えられたときのイベント
    /// </summary>
    public sealed class ChangedInput : Message<ChangedInput, InputMode>
    {
        public InputMode InputMode => this.param1;
    }
}
