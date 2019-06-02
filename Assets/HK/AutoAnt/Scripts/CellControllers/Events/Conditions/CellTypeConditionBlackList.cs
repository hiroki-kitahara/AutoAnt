using System;
using HK.AutoAnt.Constants;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers.Events
{
    /// <summary>
    /// ブラックリストに登録されていなければイベント作成可能
    /// </summary>
    [CreateAssetMenu(menuName = "AutoAnt/Cell/Event/Condition/CellType BlackList")]
    public sealed class CellTypeConditionBlackList : CellEventGenerateCondition
    {
        [SerializeField]
        private CellType[] blackList = null;
        
        public override bool Evalute(Cell[] cells)
        {
            foreach(var cell in cells)
            {
                if(Array.FindIndex(this.blackList, c => c == cell.Type) >= 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
