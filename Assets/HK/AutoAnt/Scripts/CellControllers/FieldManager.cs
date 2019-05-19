using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using static UnityEngine.Camera;

namespace HK.AutoAnt.CellControllers
{
    /// <summary>
    /// フィールドを管理するクラス
    /// </summary>
    public sealed class FieldManager : MonoBehaviour
    {
        [SerializeField]
        private Cell cellPrefab;

        private List<Cell> cells;

        void Awake()
        {
            var inputModule = InputControllers.Input.Current;

            inputModule.ClickDownAsObservable()
                .Where(x => x.ButtonId == 0)
                .SubscribeWithState(this, (x, _this) =>
                {
                    var clickableObject = this.GetClickableObject();
                    if (clickableObject != null)
                    {
                        clickableObject.OnClickDown();
                    }
                })
                .AddTo(this);

            inputModule.ClickUpAsObservable()
                .Where(x => x.ButtonId == 0)
                .SubscribeWithState(this, (x, _this) =>
                {
                    var clickableObject = this.GetClickableObject();
                    if (clickableObject != null)
                    {
                        clickableObject.OnClickUp();
                    }
                })
                .AddTo(this);

        }

        private IClickableObject GetClickableObject()
        {
            var ray = Cameraman.Instance.Camera.ScreenPointToRay(Input.mousePosition, MonoOrStereoscopicEye.Mono);
            var hitInfo = default(RaycastHit);
            if (Physics.Raycast(ray, out hitInfo))
            {
                return hitInfo.collider.GetComponent<IClickableObject>();
            }

            return null;
        }
    }
}
