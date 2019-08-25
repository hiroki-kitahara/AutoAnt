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
        public IReadOnlyReactiveProperty<double> Population => this.population;
        [SerializeField]
        private DoubleReactiveProperty population = new DoubleReactiveProperty();

        /// <summary>
        /// 人気度
        /// </summary>
        public IReadOnlyReactiveProperty<double> Popularity => this.popularity;
        [SerializeField]
        private DoubleReactiveProperty popularity = new DoubleReactiveProperty();

        /// <summary>
        /// 人口を加算する
        /// </summary>
        public void AddPopulation(double value)
        {
            this.population.Value = Math.Max(this.population.Value + value, 0.0f);
        }

        /// <summary>
        /// 人気度を加算する
        /// </summary>
        public void AddPopularity(double value)
        {
            this.popularity.Value += value;
        }
    }
}
