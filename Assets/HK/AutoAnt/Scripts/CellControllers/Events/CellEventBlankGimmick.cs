using HK.AutoAnt.CellControllers.Gimmicks;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers.Events
{
    /// <summary>
    /// <see cref="Cell"/>のイベントを持つ抽象クラス
    /// </summary>
    /// <remarks>
    /// 何もしないギミックを持つイベント特化のクラス
    /// ギミックになにか仕込みたい場合は<see cref="CellEvent"/>を継承してください
    /// </remarks>
    public abstract class CellEventBlankGimmick : CellEvent, ICellEvent
    {
        [SerializeField]
        private Blank gimmickPrefab;

        public override CellGimmickController CreateGimmickController()
        {
            return Instantiate(this.gimmickPrefab);
        }
    }
}
