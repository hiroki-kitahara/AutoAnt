using HK.AutoAnt.CellControllers.Gimmicks;
using HK.AutoAnt.Systems;
using HK.Framework.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers.Events
{
    /// <summary>
    /// クリックでお金を取得するセルイベント
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Cell/Event/AcquireMoneyOnClick")]
    public sealed class AcquireMoneyOnClick : CellEvent
    {
        /// <summary>
        /// 取得できる量
        /// </summary>
        [SerializeField]
        private int amount = 0;

        /// <summary>
        /// クリックされたらイベントを削除するか
        /// </summary>
        [SerializeField]
        private bool onClickClearEvent = false;

        public override void OnClick(Cell owner)
        {
            GameSystem.Instance.User.Wallet.AddMoney(this.amount);

            if(this.onClickClearEvent)
            {
                owner.ClearEvent();
            }
        }
    }
}
