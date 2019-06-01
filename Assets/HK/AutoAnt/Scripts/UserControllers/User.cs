using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UserControllers
{
    /// <summary>
    /// ユーザー
    /// </summary>
    public sealed class User
    {
        /// <summary>
        /// インベントリ
        /// </summary>
        public readonly Inventory Inventory = new Inventory();

        /// <summary>
        /// 財布
        /// </summary>
        public readonly Wallet Wallet = new Wallet();

        /// <summary>
        /// 街データ
        /// </summary>
        public readonly Town Town = new Town();
    }
}
