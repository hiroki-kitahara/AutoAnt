using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Database
{
    /// <summary>
    /// ゲームシステムに関するの定数
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Database/ConstantsGameSystem")]
    public sealed class ConstantsGameSystem : ScriptableObject
    {
        /// <summary>
        /// ゲーム開始時に生成されるセルのグループID
        /// </summary>
        [SerializeField]
        private int[] initialCellBundleGroups = null;
        public int[] InitialCellBundleGroups => this.initialCellBundleGroups;
    }
}
