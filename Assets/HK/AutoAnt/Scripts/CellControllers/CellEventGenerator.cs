using System;
using HK.AutoAnt.CellControllers.Events;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers
{
    /// <summary>
    /// <see cref="Cell"/>のイベントを生成する
    /// </summary>
    public sealed class CellEventGenerator
    {
        public static void Generate(Cell cell, ICellEvent cellEvent)
        {
            cell.AddEvent(cellEvent);
        }
    }
}
