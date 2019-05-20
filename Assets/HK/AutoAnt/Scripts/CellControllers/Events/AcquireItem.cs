using HK.AutoAnt.Systems;
using HK.Framework.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers.ClickEvents
{
    /// <summary>
    /// アイテムを取得するクリックイベント
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Cell/Event/AcquireItem")]
    public sealed class AcquireItem : CellClickEvent
    {
        /// <summary>
        /// 取得するアイテム名
        /// </summary>
        [SerializeField]
        private StringAsset.Finder itemName;

        /// <summary>
        /// 取得できる最小値
        /// </summary>
        [SerializeField]
        private int min;

        /// <summary>
        /// 取得できる最大値
        /// </summary>
        [SerializeField]
        private int max;

        public override void OnClick(Cell owner)
        {
            var gameSystem = GameSystem.Instance;
            var item = gameSystem.MasterData.Item.GetByName(this.itemName.Get);
            var value = Random.Range(this.min, this.max + 1);
            gameSystem.User.Inventory.AddItem(item, value);
            owner.ClearEvent();
        }
    }
}
