using UnityEngine;
using UnityEngine.Assertions;
using DG.Tweening;

namespace HK.AutoAnt
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class TweenAnimationManager : MonoBehaviour
    {
        public void Attach(GameObject target)
        {
            var animations = this.GetComponents<DOTweenAnimation>();
            foreach(var a in animations)
            {
                a.targetGO = target;
            }
        }
    }
}
