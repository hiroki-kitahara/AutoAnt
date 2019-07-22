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
        private History history = null;
        public History History => this.history;

        [SerializeField]
        private UnlockCellEvent unlockCellEvent = null;
        public UnlockCellEvent UnlockCellEvent => this.unlockCellEvent;

        [SerializeField]
        private Option option = null;
        public Option Option => this.option;

        public SerializableUser GetSerializable()
        {
            return new SerializableUser()
            {
                Wallet = this.Wallet.GetSerializable(),
                Inventory = this.Inventory,
                History = this.History,
                UnlockCellEvent = this.UnlockCellEvent,
                Option = this.Option
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
                this.history = serializableData.History;
                this.unlockCellEvent = serializableData.UnlockCellEvent;
                this.option = serializableData.Option;
            }
        }

        void ISavable.Save()
        {
            LocalSaveData.User.Save(this.GetSerializable());
        }
    }
}
