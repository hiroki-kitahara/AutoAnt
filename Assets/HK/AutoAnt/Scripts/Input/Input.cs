using HK.AutoAnt.InputControllers.Modules;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.InputControllers
{
    /// <summary>
    /// このゲームの入力制御を行うクラス
    /// </summary>
    public sealed class Input
    {
        private static IInputModule module = null;
        public static IInputModule Current
        {
            get
            {
                if(module == null)
                {
                    // TODO: プラットフォーム対応
                    module = new Standalone();
                }

                return module;
            }
        }
    }
}
