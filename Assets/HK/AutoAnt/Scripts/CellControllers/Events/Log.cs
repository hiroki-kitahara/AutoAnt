using HK.AutoAnt.CellControllers.Gimmicks;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers.Events
{
    /// <summary>
    /// セルクリック時にログを表示するイベント
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Cell/Event/Log")]
    public sealed class Log : CellEventBlankGimmick
    {
        public override void OnClick(Cell owner)
        {
            Debug.Log($"{owner.Position}", owner);
            owner.ClearEvent();
        }
    }
}
