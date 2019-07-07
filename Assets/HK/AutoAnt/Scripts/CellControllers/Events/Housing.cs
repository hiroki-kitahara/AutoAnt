using HK.AutoAnt.Database;
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
    public sealed class Housing : CellEvent, IAddTownPopulation, ILevelUpEvent, IReceiveBuff
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

        public float Buff { get; private set; } = 0.0f;

        private GameSystem gameSystem;

        private MasterDataHousingLevelParameter.Record levelParameter;

        void IAddTownPopulation.Add(GameSystem gameSystem, float deltaTime)
        {
            Assert.IsNotNull(this.levelParameter);
            var popularityRate = gameSystem.Constants.Housing.PopularityRate;
            var town = gameSystem.User.Town;
            var result = Calculator.AddPopulation(this.levelParameter.Population, town.Popularity.Value, popularityRate, 1.0f + this.Buff, deltaTime);
            this.CurrentPopulation += result;
            town.AddPopulation(result);
        }

        public override void Initialize(Vector2Int position, GameSystem gameSystem, bool isInitializingGame)
        {
            base.Initialize(position, gameSystem, isInitializingGame);
            this.gameSystem = gameSystem;
            this.gameSystem.User.Town.AddPopulation(this.CurrentPopulation);
            this.levelParameter = this.gameSystem.MasterData.HousingLevelParameter.Records.Get(this.Id, this.Level);
        }

        public override void Remove(GameSystem gameSystem)
        {
            base.Remove(gameSystem);
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
        }

        void IReceiveBuff.AddBuff(float value)
        {
            this.Buff += value;
            if(this.Buff < 0.0f)
            {
                this.Buff = 0.0f;
            }
        }
    }
}
