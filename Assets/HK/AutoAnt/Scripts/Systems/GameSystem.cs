using HK.AutoAnt.CameraControllers;
using HK.AutoAnt.CellControllers;
using HK.AutoAnt.Database;
using HK.AutoAnt.Events;
using HK.AutoAnt.GameControllers;
using HK.AutoAnt.SaveData;
using HK.AutoAnt.UserControllers;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Systems
{
    /// <summary>
    /// ゲームの根幹部分をまとめるクラス
    /// </summary>
    public sealed class GameSystem : MonoBehaviour
    {
        private static GameSystem instance;
        public static GameSystem Instance => instance;

        [SerializeField]
        private User user = null;
        private User instanceUser = null;
        public User User => this.instanceUser = this.instanceUser ?? Instantiate(this.user);

        [SerializeField]
        private MasterData masterData = null;
        public MasterData MasterData => this.masterData;

        [SerializeField]
        private Database.Constants constants = null;
        public Database.Constants Constants => this.constants;

        [SerializeField]
        private CellManager cellManager = null;
        public CellManager CellManager => this.cellManager;

        [SerializeField]
        private Cameraman cameraman = null;
        public Cameraman Cameraman => this.cameraman;

        [SerializeField]
        private GameSEController seController = null;
        public GameSEController SEController => this.seController;

        private ISavable[] Savables => new ISavable[]
        {
            this.User,
            this.CellManager
        };

        void Awake()
        {
            Assert.IsNull(instance);
            instance = this;
        }

        void Start()
        {
            foreach (var savable in this.Savables)
            {
                savable.Initialize();
            }
            
            Broker.Global.Publish(GameStart.Get(this));
        }

        void OnDestroy()
        {
            Assert.IsNotNull(instance);
            instance = null;
        }

        void OnApplicationPause(bool status)
        {
            if(status)
            {
                Broker.Global.Publish(GamePause.Get());
            }
            else
            {
                Broker.Global.Publish(GameResume.Get());
            }
        }

        void OnApplicationQuit()
        {
            Broker.Global.Publish(GameEnd.Get());

            foreach(var savable in this.Savables)
            {
                savable.Save();
            }
        }
    }
}
