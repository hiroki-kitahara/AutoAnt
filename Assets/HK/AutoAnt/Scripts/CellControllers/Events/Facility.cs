using System;
using System.Collections.Generic;
using HK.AutoAnt.Database;
using HK.AutoAnt.Events;
using HK.AutoAnt.Extensions;
using HK.AutoAnt.Systems;
using HK.AutoAnt.UI;
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
    public sealed class Facility : CellEvent, ILevelUpEvent, IReceiveBuff, IProductHolder
    {
        /// <summary>
        /// レベル
        /// </summary>
        public int Level { get; set; } = 1;

        private GameSystem gameSystem;

        public MasterDataFacilityLevelParameter.Record LevelParameter { get; private set; }

        /// <summary>
        /// 生産物を生産するタイマー
        /// </summary>
        public float ProductTimer { get; private set; } = 0.0f;

        /// <summary>
        /// 生産されるまでの残り時間のパーセンテージ
        /// </summary>
        public float RemainProductTimePercent => this.ProductTimer / this.LevelParameter.NeedProductTime;

        /// <summary>
        /// 生産されるまでの残り時間
        /// </summary>
        public float RemainProductTime => this.LevelParameter.NeedProductTime - this.ProductTimer;

        /// <summary>
        /// 生産したアイテムのリスト
        /// </summary>
        public List<int> Products { get; private set; } = new List<int>();

        public float Buff { get; private set; } = 0.0f;

        private double Popularity => this.LevelParameter.Popularity * (1.0f + this.Buff);

        public override void Initialize(Vector2Int position, GameSystem gameSystem, bool isInitializingGame)
        {
            base.Initialize(position, gameSystem, isInitializingGame);
            this.gameSystem = gameSystem;
            this.LevelParameter = this.gameSystem.MasterData.FacilityLevelParameter.Records.Get(this.Id, this.Level);
            gameSystem.User.Town.AddPopularity(this.Popularity);
            this.gameSystem.UpdateAsObservable()
                .Where(_ => this.CanProduce)
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.ProductTimer += UnityEngine.Time.deltaTime;
                    if(_this.ProductTimer >= _this.LevelParameter.NeedProductTime)
                    {
                        _this.Products.Add(_this.LevelParameter.ProductId);
                        _this.Broker.Publish(AddedFacilityProduct.Get(_this));
                        _this.ProductTimer = 0.0f;
                    }
                })
                .AddTo(this.instanceEvents);
        }

        public override void Remove(GameSystem gameSystem)
        {
            base.Remove(gameSystem);
            gameSystem.User.Town.AddPopularity(-this.Popularity);
        }

        public override void OnClick(Cell owner)
        {
            if(this.CanCollectionProducts)
            {
                this.CollectionProducts();
            }
            else
            {
                Framework.EventSystems.Broker.Global.Publish(RequestOpenCellEventDetailsPopup.Get(this));
            }
        }

        public bool CanLevelUp()
        {
            return this.CanLevelUp(this.gameSystem);
        }

        public void LevelUp()
        {
            // レベルアップ前の人気度を減算する
            var oldPopularity = this.Popularity;
            this.gameSystem.User.Town.AddPopularity(-oldPopularity);

            this.LevelUp(this.gameSystem);

            this.LevelParameter = this.gameSystem.MasterData.FacilityLevelParameter.Records.Get(this.Id, this.Level);

            // レベルアップ後の人気度を加算する
            var newPopularity = this.Popularity;
            this.gameSystem.User.Town.AddPopularity(newPopularity);
        }

        /// <summary>
        /// 生産物を生産可能か返す
        /// </summary>
        private bool CanProduce => this.Products.Count < this.LevelParameter.ProductSlot;

        /// <summary>
        /// 生産物を回収可能か返す
        /// </summary>
        private bool CanCollectionProducts => this.Products.Count > 0;

        /// <summary>
        /// 生産物をインベントリに追加する
        /// </summary>
        private void CollectionProducts()
        {
            Assert.IsTrue(this.CanCollectionProducts);

            var itemRecords = new Dictionary<MasterDataItem.Record, int>();
            foreach (var p in this.Products)
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

            this.Products.Clear();

            this.Broker.Publish(AcquiredFacilityProduct.Get(this));
        }

        public override void AttachDetailsPopup(CellEventDetailsPopup popup)
        {
            popup.AddProperty(property =>
            {
                property.Prefix.text = popup.Popularity.Get;
                property.Value.text = this.LevelParameter.Popularity.ToReadableString("###");
            });

            popup.AddProperty(property =>
            {
                property.Prefix.text = popup.Product.Get;
                property.Value.text = popup.ProductValue.Format(this.LevelParameter.ProductRecord.Name, this.LevelParameter.NeedProductTime);
            });

            this.AttachDetailsPopup(popup, this.gameSystem);
        }

        public override void UpdateDetailsPopup(CellEventDetailsPopup popup)
        {
            popup.ApplyTitle(this.EventName, this.Level);
            popup.UpdateProperties();
            popup.ClearLevelUpCosts();
            this.AttachDetailsPopup(popup, this.gameSystem);
        }
        
        void IReceiveBuff.AddBuff(float value)
        {
            var oldPopularity = this.Popularity;
            this.gameSystem.User.Town.AddPopularity(-oldPopularity);

            this.Buff += value;
            if (this.Buff < 0.0f)
            {
                this.Buff = 0.0f;
            }

            var newPopularity = this.Popularity;
            this.gameSystem.User.Town.AddPopularity(newPopularity);
        }
    }
}
