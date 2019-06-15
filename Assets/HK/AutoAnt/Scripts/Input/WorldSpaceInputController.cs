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
            Input.Current.OnDrag(eventData);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Input.Current.OnPointerClick(eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Input.Current.OnPointerDown(eventData);
        }
    }
}
