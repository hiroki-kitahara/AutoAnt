using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Database
{
    /// <summary>
    /// 全てのマスターデータを保持するクラス
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Database/MasterData")]
    public sealed class MasterData : ScriptableObject
    {
        [SerializeField]
        private MasterDataItem item;
        public MasterDataItem Item => this.item;
    }
}
