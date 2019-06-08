using System.Collections.Generic;
using System.Linq;
using HK.AutoAnt.CellControllers.Events;
using HK.AutoAnt.Database;
using HK.AutoAnt.Systems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Extensions
{
    /// <summary>
    /// <see cref="CellControllers.Events.ILevelUpEvent"/>に関する拡張関数
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// レベルアップ可能か返す
        /// </summary>
        public static bool CanLevelUp(this ILevelUpEvent self, GameSystem gameSystem)
        {
            var levelUpCostRecord = gameSystem.MasterData.LevelUpCost.Records.Get(self.Id, self.Level);
            if (levelUpCostRecord == null)
            {
                Debug.Log($"Id = {self.Id}は既にレベルMAX");
                return false;
            }

            if (!levelUpCostRecord.Cost.IsEnough(gameSystem.User, gameSystem.MasterData.Item))
            {
                Debug.Log($"Id = {self.Id}, Level = {self.Level}の必要な素材が足りない");
                return false;
            }

            return true;
        }

        public static void LevelUp(this ILevelUpEvent self, GameSystem gameSystem)
        {
            var levelUpCostRecord = gameSystem.MasterData.LevelUpCost.Records.Get(self.Id, self.Level);

            levelUpCostRecord.Cost.Consume(gameSystem.User, gameSystem.MasterData.Item);
            self.Level++;
            Debug.Log($"LevelUp -> {self.Level}");
        }
    }
}
