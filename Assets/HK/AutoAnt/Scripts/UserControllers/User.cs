using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UserControllers
{
    /// <summary>
    /// ユーザー
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/User")]
    public sealed class User : ScriptableObject
    {
        /// <summary>
        /// インベントリ
        /// </summary>
        [SerializeField]
        private Inventory inventory = null;
        public Inventory Inventory => this.inventory;

        /// <summary>
        /// 財布
        /// </summary>
        [SerializeField]
        private Wallet wallet = null;
        public Wallet Wallet => this.wallet;

        /// <summary>
        /// 街データ
        /// </summary>
        public readonly Town Town = new Town();
    }
}
