using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Events
{
    /// <summary>
    /// カメラのズーム値の変更をリクエストするイベント
    /// </summary>
    public sealed class RequestCameraZoom : Message<RequestCameraZoom, float>
    {
        /// <summary>
        /// ズーム値
        /// </summary>
        public float Value => this.param1;
    }
}
