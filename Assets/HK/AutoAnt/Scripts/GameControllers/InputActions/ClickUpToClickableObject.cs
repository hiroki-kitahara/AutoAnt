using HK.AutoAnt.CameraControllers;
using HK.AutoAnt.CellControllers;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// クリックイベントで<see cref="IClickableObject.OnClickUp"/>を実行する
    /// </summary>
    public sealed class ClickUpToClickableObject : IInputAction<InputControllers.Events.ClickData>
    {
        public void Do(InputControllers.Events.ClickData data)
        {
            var ray = Cameraman.Instance.Camera.ScreenPointToRay(data.Position);
            var clickableObject = CellManager.GetClickableObject(ray);
            if (clickableObject != null)
            {
                clickableObject.OnClickUp();
            }
        }
    }
}
