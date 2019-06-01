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
    public sealed class Housing : CellEventBlankGimmick, IAddTownPopulation
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
        public int Level = 1;

        void IAddTownPopulation.Add(Town town)
        {
            var result = (this.BasePopulationAmount * this.Level) * Mathf.FloorToInt(town.Popularity.Value / 1000);
            this.CurrentPopulation += result;
            town.AddPopulation(result);
        }

        public override void Initialize(Vector2Int position, GameSystem gameSystem)
        {
            base.Initialize(position, gameSystem);
            gameSystem.TownUpdater.AddTownPopulations.Add(this);
        }

        public override void Remove(GameSystem gameSystem)
        {
            base.Remove(gameSystem);
            gameSystem.TownUpdater.AddTownPopulations.Remove(this);
            gameSystem.User.Town.AddPopulation(-this.CurrentPopulation);
        }
    }
}
