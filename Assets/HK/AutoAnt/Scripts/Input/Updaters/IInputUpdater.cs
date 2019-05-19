using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.InputControllers.Updaters
{
    /// <summary>
    /// 入力更新処理を行うインターフェイス
    /// </summary>
    public interface IInputUpdater
    {
        void Update();
    }
}
