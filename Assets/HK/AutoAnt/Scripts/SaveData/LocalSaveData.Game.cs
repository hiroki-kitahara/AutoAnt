using HK.AutoAnt.CellControllers;
using HK.AutoAnt.SaveData.Internal;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.SaveData
{
    /// <summary>
    /// ゲーム関連のセーブデータ
    /// </summary>
    public static partial class LocalSaveData
    {
        public static readonly GameSaveData Game = new GameSaveData();

        public class GameSaveData
        {
            public readonly ISaveData<CellMapper> Mapper = new SaveData<CellMapper>("Game.CellMapper", new ES3Settings() { format = ES3.Format.JSON });
        }
    }
}
