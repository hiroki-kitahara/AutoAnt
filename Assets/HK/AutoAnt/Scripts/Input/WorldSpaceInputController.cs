using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

namespace HK.AutoAnt.InputControllers
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class WorldSpaceInputController : MonoBehaviour, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("PointerDown");
        }
    }
}
