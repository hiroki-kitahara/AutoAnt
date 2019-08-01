using HK.AutoAnt.Database;
using HK.AutoAnt.Extensions;
using HK.AutoAnt.GameControllers;
using HK.AutoAnt.Systems;
using HK.AutoAnt.UserControllers;
using UnityEngine;
using UnityEngine.Assertions;
using HK.AutoAnt.EffectSystems;
using System.Collections.Generic;
using HK.AutoAnt.Events;
using UniRx;
using HK.AutoAnt.UI;

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
    public sealed class Road : CellEvent, ILevelUpEvent, IRoad
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
            this.ObserveAddReceiveBuff();
        }

        public override void Remove(GameSystem gameSystem)
        {
            base.Remove(gameSystem);
            this.ApplyBuff(-this.levelParameter.AddBuff);
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

            // 範囲が広がることを考慮して一旦バフを解除する
            var oldBuffValue = this.levelParameter.AddBuff;
            this.ApplyBuff(-this.levelParameter.AddBuff);

            this.levelParameter = this.gameSystem.MasterData.RoadLevelParameter.Records.Get(this.Id, this.Level);

            // 新しいパラメータでバフを付与する
            this.ApplyBuff(this.levelParameter.AddBuff);
        }

        /// <summary>
        /// 範囲内の<see cref="IReceiveBuff"/>にバフを与える
        /// </summary>
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

        /// <summary>
        /// <see cref="IReceiveBuff"/>が追加されるのを監視する
        /// </summary>
        private void ObserveAddReceiveBuff()
        {
            // 追加されたIReceiveBuffに対してバフを与える
            HK.Framework.EventSystems.Broker.Global.Receive<AddedCellEvent>()
                .Where(x => this.CanApplyBuff(x.CellEvent))
                .SubscribeWithState(this, (x, _this) =>
                {
                    var receiveBuff = x.CellEvent as IReceiveBuff;
                    receiveBuff.AddBuff(_this.levelParameter.AddBuff);
                })
                .AddTo(this.instanceEvents);
        }

        /// <summary>
        /// <paramref name="target"/>にバフを適用出来るか返す
        /// </summary>
        private bool CanApplyBuff(ICellEvent target)
        {
            // そもそもIReceiveBuffを継承していない場合はなにもしない
            if(!(target is IReceiveBuff))
            {
                return false;
            }

            // 範囲内に存在するかチェック
            var impactRange = this.levelParameter.ImpactRange;
            var range = new RectInt(
                this.Origin.x - impactRange,
                this.Origin.y - impactRange,
                impactRange * 2,
                impactRange * 2
            );
            for (var y = 0; y < target.Size; y++)
            {
                for (var x = 0; x < target.Size; x++)
                {
                    var position = target.Origin + new Vector2Int(x, y);
                    if(
                        range.xMin <= position.x && position.x <= range.xMax &&
                        range.yMin <= position.y && position.y <= range.yMax
                    )
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public override void AttachFooterSelectCellEvent(FooterSelectCellEventController controller)
        {
            throw new System.NotImplementedException();
        }

        public override void UpdateFooterSelectCellEvent(FooterSelectCellEventController controller)
        {
            throw new System.NotImplementedException();
        }
    }
}
