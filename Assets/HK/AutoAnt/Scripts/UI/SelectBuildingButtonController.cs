﻿using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// フッターメニューの住宅ボタンを制御するクラス
    /// </summary>
    public sealed class SelectBuildingButtonController : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        [SerializeField]
        private FooterController footerController;

        [SerializeField]
        private Constants.CellEventCategory showCategory;

        void Awake()
        {
            this.button.OnClickAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.footerController.ShowSelectBuilding();
                })
                .AddTo(this);
        }
    }
}
