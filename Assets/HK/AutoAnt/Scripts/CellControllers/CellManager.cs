using System.Collections.Generic;
using HK.AutoAnt.CellControllers.Events;
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
        private CellEventGenerateSpec cellEventGenerateSpec = null;

        [SerializeField]
        private Transform parent = null;

        [SerializeField]
        private FieldInitializer fieldInitializer = null;

        private CellMapper cellMapper = new CellMapper();

        public CellGenerator Generator { get; private set; }

        private CellEventGenerator cellEventGenerator;

        void Awake()
        {
            this.Generator = new CellGenerator(this.cellMapper, this.parent);
            // this.cellEventGenerator = new CellEventGenerator(this, this.cellSpec, this.cellEventGenerateSpec, this.cellMapper);

            this.fieldInitializer.Generate(this);
        }

        public IClickableObject GetClickableObject(Ray ray)
        {
            var hitInfo = default(RaycastHit);
            if (Physics.Raycast(ray, out hitInfo))
            {
                return hitInfo.collider.GetComponent<IClickableObject>();
            }

            return null;
        }
    }
}
