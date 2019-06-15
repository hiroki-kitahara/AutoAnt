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
        [SerializeField]
        private InputSpec inputSpec = null;

        private IInputUpdater updater;

        // void Awake()
        // {
        //     // TODO: プラットフォーム対応
        //     this.updater = new Standalone(this.inputSpec);
        // }

        // void Update()
        // {
        //     this.updater.Update();
        // }
    }
}
