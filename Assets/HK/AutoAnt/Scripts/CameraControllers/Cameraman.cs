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
                Assert.IsNotNull(this.root);
                return this.root.position;
            }
            set
            {
                Assert.IsNotNull(this.root);
                this.root.position = value;
            }
        }

        public Vector3 Pivot
        {
            get
            {
                Assert.IsNotNull(this.pivot);
                return this.pivot.localEulerAngles;
            }
            set
            {
                Assert.IsNotNull(this.pivot);
                this.pivot.localEulerAngles = value;
            }
        }

        public Vector3 Rig
        {
            get
            {
                Assert.IsNotNull(this.rig);
                return this.rig.localEulerAngles;
            }
            set
            {
                Assert.IsNotNull(this.rig);
                this.rig.localEulerAngles = value;
            }
        }

        public float Size
        {
            get
            {
                Assert.IsNotNull(this.controlledCamera);
                return this.controlledCamera.orthographicSize;
            }
            set
            {
                Assert.IsNotNull(this.controlledCamera);
                this.controlledCamera.orthographicSize = value;
            }
        }

        public float Distance
        {
            get
            {
                Assert.IsNotNull(this.distance);
                return this.distance.localPosition.z;
            }
            set
            {
                Assert.IsNotNull(this.distance);
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
            Assert.IsNotNull(this.Camera);
            
            var t = this.Camera.transform;
            var forward = Vector3.Scale(t.forward, new Vector3(1.0f, 0.0f, 1.0f)).normalized;
            var right = t.right;
            var forwardRate = 1.0f + (1.0f - (t.rotation.eulerAngles.x / 90.0f));

            return (forward * forwardVelocity * forwardRate) + (right * rightVelocity);
        }
    }
}
