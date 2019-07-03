using HK.AutoAnt.CellControllers.Gimmicks;
using HK.AutoAnt.UI;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers.Events
{
    /// <summary>
    /// セルクリック時にログを表示するイベント
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Cell/Event/Log")]
    public sealed class Log : CellEvent
    {
        public override void ApplyDetailsPopup(CellEventDetailsPopup popup)
        {
            throw new System.NotImplementedException();
        }

        public override void OnClick(Cell owner)
        {
            Debug.Log($"{owner.Position}", owner);
        }
    }
}
