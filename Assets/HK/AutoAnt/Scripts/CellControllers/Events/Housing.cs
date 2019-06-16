﻿using HK.AutoAnt.Database;
using HK.AutoAnt.Extensions;
using HK.AutoAnt.GameControllers;
using HK.AutoAnt.Systems;
using HK.AutoAnt.UserControllers;
using UnityEngine;
using UnityEngine.Assertions;
using HK.AutoAnt.EffectSystems;

namespace HK.AutoAnt.CellControllers.Events
{
    /// <summary>
    /// 住宅のセルイベント
    /// </summary>
    /// <remarks>
    /// - やっていること
    ///     - 人口を増やす
    /// </remarks>
    [CreateAssetMenu(menuName = "AutoAnt/Cell/Event/Housing")]
    public sealed class Housing : CellEventBlankGimmick, IAddTownPopulation, ILevelUpEvent
    {
        /// <summary>
        /// 保持している人口
        /// </summary>
        [HideInInspector]
        public double CurrentPopulation = 0;

        /// <summary>
        /// レベル
        /// </summary>
        /// <remarks>
        /// 人口増加の変動に利用しています
        /// </remarks>
        public int Level { get; set; } = 1;

        private GameSystem gameSystem;

        private MasterDataHousingLevelParameter.Record levelParameter;

        void IAddTownPopulation.Add(Town town)
        {
            Assert.IsNotNull(this.levelParameter);
            var popularityRate = this.gameSystem.Constants.Housing.PopularityRate;
            var result = Calculator.AddPopulation(this.levelParameter.Population, town.Popularity.Value, popularityRate);
            this.CurrentPopulation += result;
            town.AddPopulation(result);
        }

        public override void Initialize(Vector2Int position, GameSystem gameSystem, bool isInitializingGame)
        {
            base.Initialize(position, gameSystem, isInitializingGame);
            this.gameSystem = gameSystem;
            this.gameSystem.User.Town.AddPopulation(this.CurrentPopulation);
            this.gameSystem.UserUpdater.AddTownPopulations.Add(this);
            this.levelParameter = this.gameSystem.MasterData.HousingLevelParameter.Records.Get(this.Id, this.Level);
        }

        public override void Remove(GameSystem gameSystem)
        {
            base.Remove(gameSystem);
            gameSystem.UserUpdater.AddTownPopulations.Remove(this);
            gameSystem.User.Town.AddPopulation(-this.CurrentPopulation);
        }

        public override void OnClick(Cell owner)
        {
            if(this.CanLevelUp())
            {
                this.LevelUp();
            }
        }

        public bool CanLevelUp()
        {
            return this.CanLevelUp(this.gameSystem);
        }

        public void LevelUp()
        {
            this.LevelUp(this.gameSystem);
            this.levelParameter = this.gameSystem.MasterData.HousingLevelParameter.Records.Get(this.Id, this.Level);
            this.gameSystem.User.History.GenerateCellEvent.Add(this.Id, this.Level - 1);
        }
    }
}
