using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace HK.AutoAnt.InputControllers
{
    /// <summary>
    /// ワールド空間の入力を制御するクラス
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
