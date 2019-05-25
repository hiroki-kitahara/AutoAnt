using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CameraControllers
{
    /// <summary>
    /// カメラマン
    /// </summary>
    public sealed class Cameraman : MonoBehaviour
    {
        [SerializeField]
        private Transform root = null;

        [SerializeField]
        private Transform pivot = null;

        [SerializeField]
        private Transform rig = null;

        [SerializeField]
        private Transform distance = null;

        [SerializeField]
        private Camera controlledCamera = null;
        public Camera Camera => this.controlledCamera;

        public Vector3 Position
        {
            get
            {
                return this.root.position;
            }
            set
            {
                this.root.position = value;
            }
        }

        public Vector3 Pivot
        {
            get
            {
                return this.pivot.localEulerAngles;
            }
            set
            {
                this.pivot.localEulerAngles = value;
            }
        }

        public Vector3 Rig
        {
            get
            {
                return this.rig.localEulerAngles;
            }
            set
            {
                this.rig.localEulerAngles = value;
            }
        }

        public float Distance
        {
            get
            {
                return this.distance.localPosition.z;
            }
            set
            {
                var position = this.distance.localPosition;
                position.z = value;
                this.distance.localPosition = position;
            }
        }

        /// <summary>
        /// FPS視点での移動量にして返す
        /// </summary>
        public Vector3 ToFirstPersonVector(float forwardVelocity, float rightVelocity)
        {
            var t = this.Camera.transform;
            var forward = Vector3.Scale(t.forward, new Vector3(1.0f, 0.0f, 1.0f)).normalized;
            var right = t.right;

            return (forward * forwardVelocity) + (right * rightVelocity);
        }
    }
}
