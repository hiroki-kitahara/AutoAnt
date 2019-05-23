using HK.AutoAnt.CameraControllers;
using HK.AutoAnt.CellControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// セルにイベントを追加するアクション
    /// </summary>
    public sealed class AddCellEventActions : InputActions
    {
        public AddCellEventActions(CellEventGenerator eventGenerator)
            : base(
                null,
                null,
                new ClickToAddCellEvent(eventGenerator),
                null
            )
        {
        }
    }
}
