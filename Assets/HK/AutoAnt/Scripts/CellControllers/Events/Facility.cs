using System;
using HK.AutoAnt.CellControllers.Gimmicks;
using HK.AutoAnt.Database;
using HK.AutoAnt.Extensions;
using HK.AutoAnt.GameControllers;
using HK.AutoAnt.Systems;
using HK.AutoAnt.UserControllers;
using HK.Framework.Text;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers.Events
{
    /// <summary>
    /// 施設のセルイベント
    /// </summary>
    /// <remarks>
    /// - やっていること
    ///     - 人気度の増減
    ///     - アイテムの生産
    /// </remarks>
    [CreateAssetMenu(menuName = "AutoAnt/Cell/Event/Facility")]
    public sealed class Facility : CellEventBlankGimmick, ILevelUpEvent
    {
        /// <summary>
        /// レベル
        /// </summary>
        public int Level { get; set; } = 1;

        private GameSystem gameSystem;

        private MasterDataFacilityLevelParameter.Record LevelParameter => this.gameSystem.MasterData.FacilityLevelParameter.Records.Get(this.Id, this.Level);

        public override void Initialize(Vector2Int position, GameSystem gameSystem)
        {
            base.Initialize(position, gameSystem);
            this.gameSystem = gameSystem;
            gameSystem.User.Town.AddPopularity(this.LevelParameter.Popularity);
        }

        public override void Remove(GameSystem gameSystem)
        {
            base.Remove(gameSystem);
            gameSystem.User.Town.AddPopularity(-this.LevelParameter.Popularity);
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
            // レベルアップ前の人気度を減算する
            var oldPopularity = this.LevelParameter.Popularity;
            this.gameSystem.User.Town.AddPopularity(-oldPopularity);

            this.LevelUp(this.gameSystem);

            // レベルアップ後の人気度を加算する
            var newPopularity = this.LevelParameter.Popularity;
            this.gameSystem.User.Town.AddPopularity(newPopularity);
        }
    }
}
