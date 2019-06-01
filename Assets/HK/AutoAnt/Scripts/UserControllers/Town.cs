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
        /// 総人口
        /// </summary>
        public IReadOnlyReactiveProperty<int> Population => this.population;
        private readonly ReactiveProperty<int> population = new ReactiveProperty<int>();

        /// <summary>
        /// 人気度
        /// </summary>
        public IReadOnlyReactiveProperty<int> Popularity => this.popularity;
        private readonly ReactiveProperty<int> popularity = new ReactiveProperty<int>();

        /// <summary>
        /// 人口を加算する
        /// </summary>
        public void AddPopulation(int value)
        {
            this.population.Value += value;

            Assert.IsTrue(this.Population.Value >= 0);
        }

        /// <summary>
        /// 人気度を加算する
        /// </summary>
        public void AddPopularity(int value)
        {
            this.popularity.Value += value;

            Assert.IsTrue(this.Popularity.Value >= 0);
        }
    }
}
