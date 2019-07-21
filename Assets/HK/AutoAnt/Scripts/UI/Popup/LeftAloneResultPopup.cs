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
    /// 放置によるリソース獲得を表示するポップアップを制御するクラス
    /// </summary>
    public sealed class LeftAloneResultPopup : TweenPopup
    {
        [SerializeField]
        private Button decideButton = null;
        public Button DecideButton => this.decideButton;

        [SerializeField]
        private Button adsButton = null;
        public Button AdsButton => this.adsButton;

        [SerializeField]
        private TextMeshProUGUI money = null;

        [SerializeField]
        private TextMeshProUGUI population = null;

        public void Initialize(double money, double population)
        {
            this.money.text = money.ToReadableString("###.00");
            this.population.text = population.ToReadableString("###.00");
        }
    }
}
