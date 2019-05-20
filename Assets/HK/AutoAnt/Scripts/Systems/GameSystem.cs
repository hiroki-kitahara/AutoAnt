﻿using HK.AutoAnt.CellControllers;
using HK.AutoAnt.Database;
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
        private MasterData masterData;
        public MasterData MasterData => this.masterData;

        [SerializeField]
        private CellManager cellManager;
        public CellManager CellManager => this.cellManager;

        void Awake()
        {
            Assert.IsNull(instance);
            instance = this;
        }

        void OnDestroy()
        {
            Assert.IsNotNull(instance);
            instance = null;
        }
    }
}
