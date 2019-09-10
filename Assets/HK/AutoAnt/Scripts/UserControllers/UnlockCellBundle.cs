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
        public int NextPopulation => this.nextPopulation;
        private int nextPopulation = int.MinValue;

        public void SetNextPopulation(MasterDataUnlockCellBundle masterData)
        {
            var targets = masterData.Records.Where(x => this.nextPopulation < x.NeedPopulation).ToList();
            var id = -1;
            var min = int.MaxValue;
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
