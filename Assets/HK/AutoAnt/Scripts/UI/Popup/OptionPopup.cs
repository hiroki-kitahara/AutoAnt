using System;
using DG.Tweening;
using HK.AutoAnt.Events;
using HK.AutoAnt.Extensions;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// ゲームのオプションを設定出来るポップアップを制御するクラス
    /// </summary>
    public sealed class OptionPopup : TweenPopup
    {
        [SerializeField]
        private Slider bgmSlider = null;
        public Slider BGMSlider => this.bgmSlider;

        [SerializeField]
        private Slider seSlider = null;
        public Slider SESlider => this.seSlider;

        [SerializeField]
        private Button deleteSaveDataButton = null;
        public Button DeleteSaveDataButton => this.deleteSaveDataButton;

        [SerializeField]
        private Button closeButton = null;
        public Button CloseButton => this.closeButton;
    }
}
