using UnityEngine;
using UnityEngine.Assertions;
using DG.Tweening;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// ツイーンで表示と非表示を行えるポップアップのインターフェイス
    /// </summary>
    public interface ITweenPopup
    {
        /// <summary>
        /// アニメーションリスト
        /// </summary>
        DOTweenAnimation[] TweenAnimations { get; }
    }
}
