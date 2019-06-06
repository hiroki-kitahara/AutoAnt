using System;
using System.Collections.Generic;
using HK.AutoAnt.CellControllers.Gimmicks;
using HK.AutoAnt.Database;
using HK.AutoAnt.Extensions;
using HK.AutoAnt.GameControllers;
using HK.AutoAnt.Systems;
using HK.AutoAnt.UserControllers;
using HK.Framework.Text;
using UniRx;
using UniRx.Triggers;
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

        private MasterDataFacilityLevelParameter.Record levelParameter;

        /// <summary>
        /// 生産物を生産するタイマー
        /// </summary>
        private float productTimer = 0.0f;

        /// <summary>
        /// 生産したアイテムのリスト
        /// </summary>
        private List<string> products = new List<string>();

        public override void Initialize(Vector2Int position, GameSystem gameSystem)
        {
            base.Initialize(position, gameSystem);
            this.gameSystem = gameSystem;
            this.levelParameter = this.gameSystem.MasterData.FacilityLevelParameter.Records.Get(this.Id, this.Level);
            gameSystem.User.Town.AddPopularity(this.levelParameter.Popularity);
            this.gameSystem.UpdateAsObservable()
                .Where(_ => this.CanProduce)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.productTimer += UnityEngine.Time.deltaTime;
                    if(_this.productTimer >= _this.levelParameter.NeedProductTime)
                    {
                        _this.products.Add(_this.levelParameter.ProductName);
                        _this.productTimer = 0.0f;
                    }
                })
                .AddTo(this.instanceEvents);
        }

        public override void Remove(GameSystem gameSystem)
        {
            base.Remove(gameSystem);
            gameSystem.User.Town.AddPopularity(-this.levelParameter.Popularity);
        }

        public override void OnClick(Cell owner)
        {
            if(this.CanCollectionProducts)
            {
                this.CollectionProducts();
            }
            else if(this.CanLevelUp())
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
            var oldPopularity = this.levelParameter.Popularity;
            this.gameSystem.User.Town.AddPopularity(-oldPopularity);

            this.LevelUp(this.gameSystem);

            this.levelParameter = this.gameSystem.MasterData.FacilityLevelParameter.Records.Get(this.Id, this.Level);

            // レベルアップ後の人気度を加算する
            var newPopularity = this.levelParameter.Popularity;
            this.gameSystem.User.Town.AddPopularity(newPopularity);
        }

        /// <summary>
        /// 生産物を生産可能か返す
        /// </summary>
        private bool CanProduce => this.products.Count < this.levelParameter.ProductSlot;

        /// <summary>
        /// 生産物を回収可能か返す
        /// </summary>
        private bool CanCollectionProducts => this.products.Count > 0;

        /// <summary>
        /// 生産物をインベントリに追加する
        /// </summary>
        private void CollectionProducts()
        {
            Assert.IsTrue(this.CanCollectionProducts);

            var itemRecords = new Dictionary<MasterDataItem.Record, int>();
            foreach (var p in this.products)
            {
                var itemRecord = this.gameSystem.MasterData.Item.Records.Get(p);
                if(itemRecords.ContainsKey(itemRecord))
                {
                    itemRecords[itemRecord]++;
                }
                else
                {
                    itemRecords.Add(itemRecord, 1);
                }
            }
            foreach(var i in itemRecords)
            {
                this.gameSystem.User.Inventory.AddItem(i.Key, i.Value);
            }

            this.products.Clear();
        }
    }
}
