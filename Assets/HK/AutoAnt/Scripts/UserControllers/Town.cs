using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UserControllers
{
    /// <summary>
    /// 街のデータ
    /// </summary>
    public sealed class Town
    {
        /// <summary>
        /// 人口
        /// </summary>
        public int Population => this.population.Value;

        public IReactiveProperty<int> PopulationAsObservable => this.population;

        private readonly ReactiveProperty<int> population = new ReactiveProperty<int>();

        /// <summary>
        /// 人口を加算する
        /// </summary>
        public void AddPopulation(int value)
        {
            this.population.Value += value;

            Assert.IsTrue(this.Population >= 0);
        }
    }
}
