using System.Collections.Generic;
using HK.AutoAnt.CellControllers.Events;
using HK.AutoAnt.Constants;
using HK.AutoAnt.GameControllers;
using HK.AutoAnt.SaveData;
using HK.AutoAnt.Systems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using static UnityEngine.Camera;

namespace HK.AutoAnt.CellControllers
{
    /// <summary>
    /// <see cref="Cell"/>を管理するクラス
    /// </summary>
    public sealed class CellManager : MonoBehaviour, ISavable
    {
        [SerializeField]
        private GameSystem gameSystem = null;
        
        [SerializeField]
        private Transform parent = null;

        [SerializeField]
        private FieldInitializer fieldInitializer = null;

        public CellMapper Mapper { get; private set; }

        public CellGenerator CellGenerator { get; private set; }

        public CellEventGenerator EventGenerator { get; private set; }

        void OnApplicationQuit()
        {
            LocalSaveData.Game.Mapper.Save(this.Mapper);
        }
        
        /// <summary>
        /// クリック可能なオブジェクトを返す
        /// </summary>
        /// <remarks>
        /// FIXME: ここに記述する必要がない
        /// </remarks>
        public static Cell GetCell(Ray ray)
        {
            var hitInfo = default(RaycastHit);
            if (Physics.Raycast(ray, out hitInfo))
            {
                return hitInfo.collider.GetComponent<Cell>();
            }

            return null;
        }

        void ISavable.Initialize()
        {
            var saveData = LocalSaveData.Game;
            if(saveData.Mapper.Exists())
            {
                this.Mapper = saveData.Mapper.Load();
            }
            else
            {
                this.Mapper = new CellMapper();
            }
            this.CellGenerator = new CellGenerator(this.Mapper, this.parent);
            this.EventGenerator = new CellEventGenerator(this.gameSystem, this.Mapper);

            if(!saveData.Mapper.Exists())
            {
                this.fieldInitializer.Generate(this);
            }
        }
    }
}
