using UnityEngine;
using UnityEngine.Assertions;
using static UnityEngine.Camera;

namespace HK.AutoAnt
{
    /// <summary>
    /// フィールドを管理するクラス
    /// </summary>
    public sealed class FieldManager : MonoBehaviour
    {
        [SerializeField]
        private Camera controlledCamera;

        void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                var clickableObject = this.GetClickableObject();
                if(clickableObject != null)
                {
                    clickableObject.OnClickDown();
                }
            }

            if(Input.GetMouseButtonUp(0))
            {
                var clickableObject = this.GetClickableObject();
                if(clickableObject != null)
                {
                    clickableObject.OnClickUp();
                }
            }
        }

        private IClickableObject GetClickableObject()
        {
            var ray = this.controlledCamera.ScreenPointToRay(Input.mousePosition, MonoOrStereoscopicEye.Mono);
            var hitInfo = default(RaycastHit);
            if (Physics.Raycast(ray, out hitInfo))
            {
                return hitInfo.collider.GetComponent<IClickableObject>();
            }

            return null;
        }
    }
}
