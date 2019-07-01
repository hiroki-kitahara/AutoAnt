using HK.AutoAnt.Database;
using HK.AutoAnt.Extensions;
using HK.AutoAnt.GameControllers;
using HK.AutoAnt.Systems;
using HK.AutoAnt.UserControllers;
using UnityEngine;
using UnityEngine.Assertions;
using HK.AutoAnt.EffectSystems;
using System.Collections.Generic;

namespace HK.AutoAnt.CellControllers.Events
{
    /// <summary>
    /// 道路のセルイベント
    /// </summary>
    /// <remarks>
    /// - やっていること
    ///     - 周りのセルイベントの効果を上昇させる
    /// </remarks>
    [CreateAssetMenu(menuName = "AutoAnt/Cell/Event/Road")]
    public sealed class Road : CellEvent, ILevelUpEvent
    {
        /// <summary>
        /// レベル
        /// </summary>
        /// <remarks>
        /// 人口増加の変動に利用しています
        /// </remarks>
        public int Level { get; set; } = 1;

        private GameSystem gameSystem;

        private MasterDataRoadLevelParameter.Record levelParameter;

        public override void Initialize(Vector2Int position, GameSystem gameSystem, bool isInitializingGame)
        {
            base.Initialize(position, gameSystem, isInitializingGame);
            this.gameSystem = gameSystem;
            this.levelParameter = this.gameSystem.MasterData.RoadLevelParameter.Records.Get(this.Id, this.Level);
            this.ApplyBuff(this.levelParameter.AddBuff);
        }

        public override void Remove(GameSystem gameSystem)
        {
            base.Remove(gameSystem);
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
            this.gameSystem.User.History.GenerateCellEvent.Add(this.Id, this.Level - 1);
            
            var oldBuffValue = this.levelParameter.AddBuff;
            this.levelParameter = this.gameSystem.MasterData.RoadLevelParameter.Records.Get(this.Id, this.Level);
            this.ApplyBuff(this.levelParameter.AddBuff - oldBuffValue);
        }

        private void ApplyBuff(float addValue)
        {
            var receiveBuffs = new List<IReceiveBuff>();
            Vector2IntUtility.GetRange(this.Origin, this.levelParameter.ImpactRange, (pos) =>
            {
                var cellEventMap = this.gameSystem.CellManager.Mapper.CellEvent.Map;
                if (!cellEventMap.ContainsKey(pos))
                {
                    return false;
                }

                var receiveBuff = cellEventMap[pos] as IReceiveBuff;
                if (receiveBuff == null)
                {
                    return false;
                }

                if (receiveBuffs.Contains(receiveBuff))
                {
                    return false;
                }

                receiveBuffs.Add(receiveBuff);

                return true;
            });

            foreach (var b in receiveBuffs)
            {
                b.AddBuff(addValue);
            }
        }
    }
}
