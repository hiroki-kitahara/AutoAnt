using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers.Events
{
    /// <summary>
    /// 常に作成可能な条件
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Cell/Event/Condition/True")]
    public sealed class TrueCondition : CellEventGenerateCondition
    {
        public override bool Evalute(Cell[] cells)
        {
            return true;
        }
    }
}
