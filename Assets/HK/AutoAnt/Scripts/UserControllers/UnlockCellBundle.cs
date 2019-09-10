using System;
using System.Linq;
using HK.AutoAnt.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UserControllers
{
    /// <summary>
    /// ユーザーデータに必要なUnlockCellBundleデータ
    /// </summary>
    [Serializable]
    public sealed class UnlockCellBundle
    {
        /// <summary>
        /// 次にアンロックする人口数
        /// </summary>
        public double NextPopulation => this.nextPopulation;
        private double nextPopulation = int.MinValue;

        /// <summary>
        /// 次にアンロックする人口数を設定する
        /// </summary>
        public void SetNextPopulation(MasterDataUnlockCellBundle masterData)
        {
            // 現状の次の人口数より大きい値のレコードを取得する
            var targets = masterData.Records.Where(x => this.nextPopulation < x.NeedPopulation).ToList();

            // レコードが無い場合は最大値に設定する
            if(targets.Count <= 0)
            {
                this.nextPopulation = double.MaxValue;
                return;
            }

            // 取得したレコードから最小値のレコードを対象とする
            var id = -1;
            var min = double.MaxValue;
            for (var i = 0; i < targets.Count; i++)
            {
                if(min > targets[i].NeedPopulation)
                {
                    id = i;
                    min = targets[i].NeedPopulation;
                }
            }

            this.nextPopulation = targets[id].NeedPopulation;
        }
    }
}
