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
        private Transform parent = null;

        [SerializeField]
        private FieldInitializer fieldInitializer = null;

        public CellMapper Mapper { get; private set; } = new CellMapper();

        public CellGenerator Generator { get; private set; }

        public CellEventGenerator EventGenerator { get; private set; }

        void Awake()
        {
            this.Generator = new CellGenerator(this.Mapper, this.parent);
            this.EventGenerator = new CellEventGenerator();

            this.fieldInitializer.Generate(this);
        }

        /// <summary>
        /// クリック可能なオブジェクトを返す
        /// </summary>
        /// <remarks>
        /// FIXME: ここに記述する必要がない
        /// </remarks>
        public static Cell GetCell(Ray ray)
        {
            var hitInfo = default(RaycastHit);
            if (Physics.Raycast(ray, out hitInfo))
            {
                return hitInfo.collider.GetComponent<Cell>();
            }

            return null;
        }
    }
}
