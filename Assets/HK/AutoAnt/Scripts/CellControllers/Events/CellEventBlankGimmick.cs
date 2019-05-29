using HK.AutoAnt.CellControllers.Gimmicks;
using HK.AutoAnt.Systems;
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
        private Blank gimmickPrefab = null;

        public override CellGimmickController CreateGimmickController()
        {
            var gimmick = Instantiate(this.gimmickPrefab);
            var constants = GameSystem.Instance.MasterData.Cell.Constants;
            var constantScale = constants.EffectScale;
            gimmick.transform.position = new Vector3(this.Position.x * (constants.Scale.x + constants.Interval), 0.0f, this.Position.y * (constants.Scale.z + constants.Interval));
            gimmick.transform.localScale = constantScale * this.size;

            return gimmick;
        }
    }
}
