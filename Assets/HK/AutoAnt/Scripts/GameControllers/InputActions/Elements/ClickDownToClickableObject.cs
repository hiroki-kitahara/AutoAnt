using HK.AutoAnt.CameraControllers;
using HK.AutoAnt.CellControllers;
using HK.AutoAnt.Systems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// クリックダウン時に<see cref="IClickableObject.OnClickDown"/>を実行する
    /// </summary>
    public sealed class ClickDownToClickableObject : IInputAction<InputControllers.Events.ClickData>
    {
        public void Do(InputControllers.Events.ClickData data)
        {
            var ray = GameSystem.Instance.Cameraman.Camera.ScreenPointToRay(data.Position);
            var clickableObject = Cell.GetCell(ray);
            if (clickableObject != null)
            {
                clickableObject.OnClickDown();
            }
        }
    }
}
