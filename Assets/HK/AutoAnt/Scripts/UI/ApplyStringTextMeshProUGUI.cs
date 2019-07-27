using HK.Framework.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// 文字列を<see cref="TextMeshProUGUI"/>に適用する
    /// </summary>
    public sealed class ApplyStringTextMeshProUGUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI target = null;

        [SerializeField]
        private StringAsset.Finder message = null;

        void Start()
        {
            Assert.IsNotNull(this.target);
            Assert.IsNotNull(this.message);

            this.target.text = this.message.Get;
        }

#if UNITY_EDITOR
        void Reset()
        {
            this.target = this.GetComponent<TextMeshProUGUI>();
        }

        void OnValidate()
        {
            if(this.target == null || this.message == null)
            {
                return;
            }

            if(!this.message.IsValid)
            {
                return;
            }

            this.target.text = this.message.Get;
        }
#endif
    }
}
