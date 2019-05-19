﻿using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CameraControllers
{
    /// <summary>
    /// カメラマン
    /// </summary>
    public sealed class Cameraman : MonoBehaviour
    {
        [SerializeField]
        private Transform root;
        public Transform Root => this.root;

        [SerializeField]
        private Transform pivot;

        [SerializeField]
        private Transform rig;

        [SerializeField]
        private Transform distance;

        [SerializeField]
        private Camera controlledCamera;
        public Camera Camera => this.controlledCamera;

        public static Cameraman Instance { get; private set; }

        void Awake()
        {
            Assert.IsNull(Instance);
            Instance = this;
        }

        void OnDestroy()
        {
            Assert.IsNotNull(Instance);
            Instance = null;
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
