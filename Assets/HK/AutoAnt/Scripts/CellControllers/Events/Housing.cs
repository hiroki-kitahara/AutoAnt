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
            // FIXME: 正式な計算式を適用する
            var result = Mathf.FloorToInt((this.BasePopulationAmount * (this.Level / 10.0f)) * (town.Popularity.Value / 1000.0f));
            this.CurrentPopulation += result;
            town.AddPopulation(result);
        }

        public override void Initialize(Vector2Int position, GameSystem gameSystem)
        {
            base.Initialize(position, gameSystem);
            gameSystem.UserUpdater.AddTownPopulations.Add(this);
        }

        public override void Remove(GameSystem gameSystem)
        {
            base.Remove(gameSystem);
            gameSystem.UserUpdater.AddTownPopulations.Remove(this);
            gameSystem.User.Town.AddPopulation(-this.CurrentPopulation);
        }
    }
}
