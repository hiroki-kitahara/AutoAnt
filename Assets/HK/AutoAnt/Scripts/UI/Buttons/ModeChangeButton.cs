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
        private Text buttonLabel = null;

        [SerializeField]
        private Button mySelf = null;

        void Awake()
        {

            mySelf.onClick.AddListener(() =>
            {
                Broker.Global.Publish(RequestChangeInputMode.Get());
            });

            LabelInitialize();

            Broker.Global.Receive<ChangedInputMode>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.buttonLabel.text = _this.modeLabels[x.InputMode];
                })
                .AddTo(this);
        }

        private void LabelInitialize()
        {
            modeLabels[InputMode.ClickMode] = "クリックモード";
            modeLabels[InputMode.BuildMode] = "建築モード";
            modeLabels[InputMode.DismantleMode] = "解体モード";
            modeLabels[InputMode.ExploringMode] = "開拓モード";
        }
    }
}


