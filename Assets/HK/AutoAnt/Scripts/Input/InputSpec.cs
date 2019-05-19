using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt
{
    /// <summary>
    /// 入力処理の基本情報
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Input/Spec")]
    public sealed class InputSpec : ScriptableObject
    {
        [SerializeField]
        private float dragThrehold = 1.0f;
        public float DragThrehold => this.dragThrehold;
    }
}
