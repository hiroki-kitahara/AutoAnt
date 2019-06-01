using System;
using HK.AutoAnt.CellControllers.Gimmicks;
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
    public sealed class Facility : CellEventBlankGimmick, IAddTownPopularity
    {
        /// <summary>
        /// 加算する人気度
        /// </summary>
        public int PopularityAmount = 0;

        /// <summary>
        /// レベル
        /// </summary>
        public int Level = 1;

        /// <summary>
        /// 獲得できるアイテムのレコードID
        /// </summary>
        public int AcquireItemRecordId = 0;

        /// <summary>
        /// 保持している人気度
        /// </summary>
        public int CurrentPopularity;

        /// <summary>
        /// 保持しているアイテムの数
        /// </summary>
        public int CurrentItemNumber;

        public override void Initialize(Vector2Int position, GameSystem gameSystem)
        {
            base.Initialize(position, gameSystem);
            gameSystem.UserUpdater.AddTownPopularities.Add(this);
        }

        public override void Remove(GameSystem gameSystem)
        {
            base.Remove(gameSystem);
            gameSystem.UserUpdater.AddTownPopularities.Remove(this);
            gameSystem.User.Town.AddPopularity(-this.CurrentPopularity);
        }

        void IAddTownPopularity.Add(Town town)
        {
            town.AddPopularity(this.PopularityAmount);
            this.CurrentPopularity += this.PopularityAmount;
        }
    }
}
