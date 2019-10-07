using HK.AutoAnt.GameControllers;
using HK.AutoAnt.SaveData;
using HK.AutoAnt.Systems;
using UnityEngine;

namespace HK.AutoAnt.CellControllers
{
    /// <summary>
    /// <see cref="Cell"/>を管理するクラス
    /// </summary>
    public sealed class CellManager : MonoBehaviour, ISavable
    {
        [SerializeField]
        private Transform parent = null;

        public CellMapper Mapper { get; private set; }

        public CellGenerator CellGenerator { get; private set; }

        public CellEventGenerator EventGenerator { get; private set; }
        
        void ISavable.Save()
        {
            LocalSaveData.Game.Mapper.Save(this.Mapper.GetSerializable());
        }

        void ISavable.Initialize()
        {
            var saveData = LocalSaveData.Game;
            this.Mapper = new CellMapper();
            this.CellGenerator = new CellGenerator(this.Mapper, this.parent);
            this.EventGenerator = new CellEventGenerator(this.Mapper);

            if(saveData.Mapper.Exists())
            {
                this.Mapper.Deserialize(saveData.Mapper.Load(), this.CellGenerator, this.EventGenerator);
            }
            else
            {
                foreach(var group in GameSystem.Instance.Constants.GameSystem.InitialCellBundleGroups)
                {
                    this.CellGenerator.GenerateFromCellBundle(group);
                }
            }
        }
    }
}
