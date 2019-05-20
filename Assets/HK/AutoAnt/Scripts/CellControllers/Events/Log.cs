using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers.ClickEvents
{
    /// <summary>
    /// セルクリック時にログを表示するイベント
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Cell/Event/Log")]
    public sealed class Log : CellClickEvent
    {
        public override void OnClick(Cell owner)
        {
            Debug.Log($"{owner.Id}", owner);
            owner.ClearEvent();
        }
    }
}
