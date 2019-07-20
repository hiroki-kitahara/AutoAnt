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
        private Image target;

        [SerializeField]
        private CellEventCategory category;

        void Start()
        {
            this.target.color = GameSystem.Instance.Constants.Color.CellEvent.Get(this.category);
        }
    }
}
