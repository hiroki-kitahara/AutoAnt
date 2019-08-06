using UnityEngine;
using UnityEngine.Assertions;
using HK.AutoAnt.Constants;
using UnityEngine.UI;
using HK.AutoAnt.Systems;

namespace HK.AutoAnt
{
    /// <summary>
    /// <see cref="ConstantsColor.CellEventColors"/>を<see cref="Image"/>に適用する
    /// </summary>
    public sealed class ApplyCellEventColorImage : MonoBehaviour
    {
        [SerializeField]
        private Image target = null;

        [SerializeField]
        private CellEventCategory category = CellEventCategory.Housing;

        void Start()
        {
            this.target.color = GameSystem.Instance.Constants.Color.CellEvent.Get(this.category);
        }

#if UNITY_EDITOR
        void Reset()
        {
            this.target = this.GetComponent<Image>();
        }
#endif
    }
}
