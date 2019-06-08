using System.Collections.Generic;
using HK.AutoAnt.CellControllers.Events;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.SaveData.Serializables
{
    /// <summary>
    /// <see cref="CellMapper"/>をシリアライズ可能にするクラス
    /// </summary>
    public sealed class SerializableCellMapper
    {
        public List<SerializableCell> Cells { get; set; } = new List<SerializableCell>();

        public List<ICellEvent> CellEvents { get; set; } = new List<ICellEvent>();
    }
}
