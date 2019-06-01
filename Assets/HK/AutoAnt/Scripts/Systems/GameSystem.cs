using HK.AutoAnt.CameraControllers;
using HK.AutoAnt.CellControllers;
using HK.AutoAnt.Database;
using HK.AutoAnt.GameControllers;
using HK.AutoAnt.UserControllers;
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

        public readonly User User = new User();

        [SerializeField]
        private TownUpdater townUpdater = null;

        [SerializeField]
        private MasterData masterData = null;
        public MasterData MasterData => this.masterData;

        [SerializeField]
        private CellManager cellManager = null;
        public CellManager CellManager => this.cellManager;

        [SerializeField]
        private Cameraman cameraman = null;
        public Cameraman Cameraman => this.cameraman;

        void Awake()
        {
            Assert.IsNull(instance);
            instance = this;

            this.townUpdater.Initialize(this.gameObject);
        }

        void OnDestroy()
        {
            Assert.IsNotNull(instance);
            instance = null;
        }
    }
}
