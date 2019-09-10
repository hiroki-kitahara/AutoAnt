using System;
using System.Collections.Generic;
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
        /// 次に開放される<see cref="MasterDataUnlockCellBundle"/>のIDリスト
        /// </summary>
        public List<int> TargetRecordIds => this.targetRecordIds;
        private List<int> targetRecordIds = new List<int>();

        /// <summary>
        /// アンロック処理を行えるか返す
        /// </summary>
        public bool CanUnlock => this.targetRecordIds.Count > 0;

        /// <summary>
        /// 次にアンロックする人口数を設定する
        /// </summary>
        public void SetNextPopulation(MasterDataUnlockCellBundle masterData)
        {
            this.targetRecordIds.Clear();

            // 現状の次の人口数より大きい値のレコードを取得する
            var targets = masterData.Records.Where(x => this.nextPopulation < x.NeedPopulation).ToList();

            // レコードが無い場合は最大値に設定する
            if(targets.Count <= 0)
            {
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

            foreach(var r in targets)
            {
                if(r.NeedPopulation != this.nextPopulation)
                {
                    continue;
                }

                this.targetRecordIds.Add(r.Id);
            }
        }
    }
}
