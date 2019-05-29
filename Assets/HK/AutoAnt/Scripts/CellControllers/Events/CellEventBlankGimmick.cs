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
            var position = new Vector3(this.Origin.x * (constants.Scale.x + constants.Interval), 0.0f, this.Origin.y * (constants.Scale.z + constants.Interval));
            var fixedSize = this.size - 1;
            position += new Vector3((constants.Scale.x / 2.0f) * fixedSize, 0.0f, (constants.Scale.z / 2.0f) * fixedSize);
            position += new Vector3(constants.Interval * fixedSize, 0.0f, constants.Interval * fixedSize);
            gimmick.transform.position = position;
            gimmick.transform.localScale = constants.EffectScale * this.size + (Vector3.one * (constants.Interval * fixedSize));

            return gimmick;
        }
    }
}
