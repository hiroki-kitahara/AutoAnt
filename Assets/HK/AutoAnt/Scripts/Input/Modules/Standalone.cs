using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.InputControllers.Modules
{
    /// <summary>
    /// スタンドアローンの入力制御クラス
    /// </summary>
    public sealed class Standalone : IInputModule
    {
        public bool GetClick(int button)
        {
            return UnityEngine.Input.GetMouseButton(button);
        }

        public bool GetClickDown(int button)
        {
            return UnityEngine.Input.GetMouseButtonDown(button);
        }

        public bool GetClickUp(int button)
        {
            return UnityEngine.Input.GetMouseButtonUp(button);
        }
    }
}
