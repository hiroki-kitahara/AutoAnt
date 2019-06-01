using System;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UserControllers
{
    /// <summary>
    /// 街のデータ
    /// </summary>
    [Serializable]
    public sealed class Town
    {
        /// <summary>
        /// 総人口
        /// </summary>
        public IReadOnlyReactiveProperty<int> Population => this.population;
        [SerializeField]
        private IntReactiveProperty population = new IntReactiveProperty();

        /// <summary>
        /// 人気度
        /// </summary>
        public IReadOnlyReactiveProperty<int> Popularity => this.popularity;
        [SerializeField]
        private IntReactiveProperty popularity = new IntReactiveProperty();

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
