using HK.AutoAnt.Database;
using HK.AutoAnt.Extensions;
using HK.AutoAnt.GameControllers;
using HK.AutoAnt.Systems;
using HK.AutoAnt.UserControllers;
using UnityEngine;
using UnityEngine.Assertions;
using HK.AutoAnt.EffectSystems;
using HK.Framework.EventSystems;
using HK.AutoAnt.Events;
using HK.AutoAnt.UI;
using UniRx.Triggers;
using UniRx;

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
    public sealed class Housing : CellEvent,
        IAddTownPopulation,
        ILevelUpEvent,
        IHousing,
        IReceiveBuff,
        IOpenCellEventDetailsPopup,
        IFooterSelectCellEvent
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

        double IHousing.CurrentPopulation => this.CurrentPopulation;

        double IHousing.BasePopulation => this.levelParameter.Population;
        
        public float Buff { get; private set; } = 0.0f;

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

        public override void Initialize(Vector2Int position, bool isInitializingGame)
        {
            var gameSystem = GameSystem.Instance;
            base.Initialize(position, isInitializingGame);
            gameSystem.User.Town.AddPopulation(this.CurrentPopulation);
            this.levelParameter = gameSystem.MasterData.HousingLevelParameter.Records.Get(this.Id, this.Level);
        }

        public override void Remove()
        {
            base.Remove();
            GameSystem.Instance.User.Town.AddPopulation(-this.CurrentPopulation);
        }

        public override void OnClick(Cell owner)
        {
            Framework.EventSystems.Broker.Global.Publish(RequestOpenCellEventDetailsPopup.Get(this));
        }

        public bool CanLevelUp()
        {
            return this.CanLevelUp(GameSystem.Instance);
        }

        public void LevelUp()
        {
            var gameSystem = GameSystem.Instance;
            this.LevelUp(gameSystem);
            this.levelParameter = gameSystem.MasterData.HousingLevelParameter.Records.Get(this.Id, this.Level);
        }

        void IReceiveBuff.AddBuff(float value)
        {
            this.Buff += value;
            if(this.Buff < 0.0f)
            {
                this.Buff = 0.0f;
            }
        }

        void IOpenCellEventDetailsPopup.Attach(CellEventDetailsPopup popup)
        {
            var population = popup.AddProperty(property =>
            {
                property.Prefix.text = popup.Population.Get;
                property.Value.text = this.CurrentPopulation.ToReadableString("###");
            });

            popup.AddProperty(property =>
            {
                property.Prefix.text = popup.BasePopulation.Get;
                property.Value.text = this.levelParameter.Population.ToReadableString("###");
            });

            popup.UpdateAsObservable()
                .SubscribeWithState(population, (_, _population) =>
                {
                    _population.UpdateProperty();
                })
                .AddTo(popup);

            this.AttachDetailsPopup(popup, GameSystem.Instance);
        }

        void IOpenCellEventDetailsPopup.Update(CellEventDetailsPopup popup)
        {
            popup.ApplyTitle(this.EventName, this.Level);
            popup.UpdateProperties();
            popup.ClearLevelUpCosts();
            this.AttachDetailsPopup(popup, GameSystem.Instance);
        }

        void IFooterSelectCellEvent.Attach(FooterSelectCellEventController controller)
        {
            this.AttachFooterSelectCellEvent(controller, GameSystem.Instance);
        }
    }
}
