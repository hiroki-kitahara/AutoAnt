using System.Collections.Generic;
using HK.AutoAnt.CellControllers.ClickEvents;
using HK.AutoAnt.Constants;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using static UnityEngine.Camera;

namespace HK.AutoAnt.CellControllers
{
    /// <summary>
    /// <see cref="Cell"/>を管理するクラス
    /// </summary>
    public sealed class CellManager : MonoBehaviour
    {
        [SerializeField]
        private CellSpec cellSpec;

        [SerializeField]
        private CellEventGenerateSpec cellEventGenerateSpec;

        [SerializeField]
        private Transform parent;

        [SerializeField]
        private int initialRange;

        private CellMapper cellMapper = new CellMapper();

        private CellGenerator cellGenerator;

        private CellEventGenerator cellEventGenerator;

        void Awake()
        {
            this.cellGenerator = new CellGenerator(this.cellSpec);
            this.cellEventGenerator = new CellEventGenerator(this, this.cellSpec, this.cellEventGenerateSpec, this.cellMapper);

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

            inputModule.DragAsObservable()
                .SubscribeWithState(this, (x, _this) =>
                {
                    var cameraman = Cameraman.Instance;
                    var forward = Vector3.Scale(cameraman.Camera.transform.forward, new Vector3(1.0f, 0.0f, 1.0f)).normalized;
                    var right = cameraman.Camera.transform.right;
                    Cameraman.Instance.Root.position -= ((forward * x.DeltaPosition.y) + (right * x.DeltaPosition.x)) * 0.05f;
                })
                .AddTo(this);

            for (var x = -this.initialRange; x <= this.initialRange; x++)
            {
                for (var y = -this.initialRange; y <= this.initialRange; y++)
                {
                    this.GenerateCell(new Vector2Int(x, y), CellType.Grassland, null);
                }
            }
        }

        public void GenerateCell(Vector2Int id, CellType cellType, ICellClickEvent clickEvent)
        {
            var cell = this.cellGenerator.Generate(id, cellType, this.parent, clickEvent, this.cellMapper);
            this.cellMapper.Add(cell);
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
