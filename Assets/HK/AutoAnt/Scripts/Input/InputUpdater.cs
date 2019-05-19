using HK.AutoAnt.InputControllers.Updaters;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.InputControllers
{
    /// <summary>
    /// 入力更新処理を行うクラス
    /// </summary>
    public sealed class InputUpdater : MonoBehaviour
    {
        private IInputUpdater updater;

        void Awake()
        {
            // TODO: プラットフォーム対応
            this.updater = new Standalone();
        }

        void Update()
        {
            this.updater.Update();
        }
    }
}
