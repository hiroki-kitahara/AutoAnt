using HK.AutoAnt.GameControllers;
using HK.AutoAnt.SaveData;
using HK.AutoAnt.SaveData.Serializables;
using HK.AutoAnt.Systems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UserControllers
{
    /// <summary>
    /// ユーザー
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/User")]
    public sealed class User : ScriptableObject, ISavable
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
        [SerializeField]
        private Town town = null;
        public Town Town => this.town;

        [SerializeField]
        private GenerateCellEventHistory generateCellEventHistory = null;
        public GenerateCellEventHistory GenerateCellEventHistory => this.generateCellEventHistory;

        [SerializeField]
        private UnlockCellEvent unlockCellEvent = null;
        public UnlockCellEvent UnlockCellEvent => this.unlockCellEvent;

        public SerializableUser GetSerializable()
        {
            return new SerializableUser()
            {
                Wallet = this.Wallet.GetSerializable(),
                Inventory = this.Inventory,
                GenerateCellEventHistory = this.GenerateCellEventHistory,
                UnlockCellEvent = this.UnlockCellEvent
            };
        }

        void ISavable.Initialize()
        {
            var saveData = LocalSaveData.User;
            if(saveData.Exists())
            {
                var serializableData = saveData.Load();
                this.wallet.Deserialize(serializableData.Wallet);
                this.inventory = serializableData.Inventory;
                this.generateCellEventHistory = serializableData.GenerateCellEventHistory;
                this.unlockCellEvent = serializableData.UnlockCellEvent;
            }

            this.unlockCellEvent.StartObserve(GameSystem.Instance);
        }

        void ISavable.Save()
        {
            LocalSaveData.User.Save(this.GetSerializable());
        }
    }
}
