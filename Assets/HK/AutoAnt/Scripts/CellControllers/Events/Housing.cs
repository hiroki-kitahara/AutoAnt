using HK.AutoAnt.Extensions;
using HK.AutoAnt.GameControllers;
using HK.AutoAnt.Systems;
using HK.AutoAnt.UserControllers;
using UnityEngine;
using UnityEngine.Assertions;

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
        /// ベース人口増加量
        /// </summary>
        public int BasePopulationAmount = 0;

        /// <summary>
        /// 保持している人口
        /// </summary>
        [HideInInspector]
        public int CurrentPopulation = 0;

        /// <summary>
        /// レベル
        /// </summary>
        /// <remarks>
        /// 人口増加の変動に利用しています
        /// </remarks>
        public int Level { get; set; } = 1;

        private GameSystem gameSystem;

        void IAddTownPopulation.Add(Town town)
        {
            // FIXME: 正式な計算式を適用する
            var result = Mathf.FloorToInt((this.BasePopulationAmount * (this.Level / 10.0f)) * (town.Popularity.Value / 1000.0f));
            this.CurrentPopulation += result;
            town.AddPopulation(result);
        }

        public override void Initialize(Vector2Int position, GameSystem gameSystem, bool isInitializeGame)
        {
            base.Initialize(position, gameSystem, isInitializeGame);
            this.gameSystem = gameSystem;
            this.gameSystem.User.Town.AddPopulation(this.CurrentPopulation);
            this.gameSystem.UserUpdater.AddTownPopulations.Add(this);
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
        }
    }
}
