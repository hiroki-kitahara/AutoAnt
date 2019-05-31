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
        public readonly Inventory Inventory = new Inventory();

        /// <summary>
        /// 財布
        /// </summary>
        public readonly Wallet Wallet = new Wallet();
    }
}
