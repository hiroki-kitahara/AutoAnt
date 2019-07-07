using DG.Tweening;
using HK.AutoAnt.Extensions;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// 表示非表示をアニメーションするポップアップ
    /// </summary>
    public abstract class TweenPopup : Popup, ITweenPopup
    {
        [SerializeField]
        private GameObject tweenAnimationHolder = null;

        private DOTweenAnimation[] tweenAnimations;
        DOTweenAnimation[] ITweenPopup.TweenAnimations
        {
            get
            {
                if(this.tweenAnimations == null)
                {
                    Assert.IsNotNull(this.tweenAnimationHolder);
                    this.tweenAnimations = this.tweenAnimationHolder.GetComponentsInChildren<DOTweenAnimation>();
                }

                return this.tweenAnimations;
            }
        }

        public override void Open()
        {
            this.StartTweeningOpen();
        }

        public override void Close()
        {
            this.StartTweeningClose();
        }
    }
}
