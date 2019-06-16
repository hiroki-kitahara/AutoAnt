using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HK.AutoAnt.Systems;
using HK.AutoAnt.Events;
using HK.AutoAnt.Constants;
using HK.Framework.EventSystems;
using UniRx;

namespace HK.AutoAnt
{
    public sealed class ModeChangeButton : MonoBehaviour
    {
        Dictionary<InputMode, string> modeLabels = new Dictionary<InputMode, string>();

        [SerializeField]
        private Text buttonLabel;

        void Awake()
        {

            LabelInitialize();

            Broker.Global.Receive<ChangedInput>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    buttonLabel.text = modeLabels[x.InputMode];
                })
                .AddTo(this);
        }

        public void publishClickEvent()
        {
            Broker.Global.Publish(RequestChangeInput.Get());
        }

        private void LabelInitialize()
        {
            modeLabels[InputMode.clickMode] = "クリックモード";
            modeLabels[InputMode.buildMode] = "建築モード";
            modeLabels[InputMode.dismantleMode] = "解体モード";
            modeLabels[InputMode.exploringMode] = "開拓モード";
        }
    }
}


