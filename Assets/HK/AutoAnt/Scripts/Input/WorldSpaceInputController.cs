using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace HK.AutoAnt.InputControllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class WorldSpaceInputController :
        MonoBehaviour,
        IPointerDownHandler,
        IPointerClickHandler,
        IDragHandler
    {
        public void OnDrag(PointerEventData eventData)
        {
            Input.Current.Broker.Publish(Events.Drag.Get(Events.DragData.Get(eventData.delta)));
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(eventData.dragging)
            {
                return;
            }

            Input.Current.Broker.Publish(Events.ClickUp.Get(Events.ClickData.Get(eventData.pointerId, UnityEngine.Input.mousePosition)));
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Input.Current.Broker.Publish(Events.ClickDown.Get(Events.ClickData.Get(eventData.pointerId, UnityEngine.Input.mousePosition)));
        }
    }
}
