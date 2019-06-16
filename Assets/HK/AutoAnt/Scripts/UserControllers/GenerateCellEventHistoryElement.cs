using System.Collections.Generic;
using HK.AutoAnt.Database;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UserControllers
{
    /// <summary>
    /// <see cref="GenerateCellEventHistory"/>の要素
    /// </summary>
    public class GenerateCellEventHistoryElement
    {
        /// <summary>
        /// 建設した数
        /// </summary>
        /// <remarks>
        /// レベルごとに建設した数を保持しています
        /// [0]はレベル1の建設した数になります
        /// </remarks>
        public IReadOnlyList<int> Numbers => this.numbers;
        private List<int> numbers = new List<int>();

        public void Add(int level)
        {
            while (this.numbers.Count - 1 < level)
            {
                this.numbers.Add(0);
            }

            this.numbers[level]++;
        }

        /// <summary>
        /// アンロック可能か返す
        /// </summary>
        public bool IsEnough(MasterDataUnlockCellEvent.Record.NeedCellEvent need)
        {
            // 指定されたレベルを一度も生成したことが無い場合はアンロック出来ない
            if (this.numbers.Count < need.Level)
            {
                return false;
            }

            return this.numbers[need.Level - 1] >= need.Number;
        }
    }
}
