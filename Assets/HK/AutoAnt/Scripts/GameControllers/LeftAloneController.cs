using System;
using HK.AutoAnt.Events;
using HK.AutoAnt.Systems;
using HK.AutoAnt.UI;
using HK.Framework.EventSystems;
using HK.Framework.Text;
using UniRx;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Assertions;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// ゲームを放置した際の処理を行うクラス
    /// </summary>
    public sealed class LeftAloneController : MonoBehaviour
    {
        [SerializeField]
        private UserUpdater userUpdater = null;

        [SerializeField]
        private LeftAloneResultPopup popupPrefab = null;

        /// <summary>
        /// 放置時間を更新する間隔（秒）
        /// </summary>
        [SerializeField]
        private float updateInterval = 1.0f;

        /// <summary>
        /// 放置した時間の制限値
        /// </summary>
        [SerializeField]
        private double leftAloneProcessSeconds = 100.0f;

        /// <summary>
        /// 広告を見た際のリソース獲得量の倍率
        /// </summary>
        [SerializeField]
        private int adsAcquireRate = 1;

        [SerializeField]
        private StringAsset.Finder leftAloneLocalNotificationTitle = null;

        [SerializeField]
        private StringAsset.Finder leftAloneLocalNotificationMessage = null;

        void Awake()
        {
            Broker.Global.Receive<GameStart>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.OnLeftAlone();
                })
                .AddTo(this);

            Broker.Global.Receive<GameEnd>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.OnGameLeft();
                    _this.RegisterLeftAloneLocalNotification();
                })
                .AddTo(this);

            Broker.Global.Receive<GamePause>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.OnGameLeft();
                    _this.RegisterLeftAloneLocalNotification();
                })
                .AddTo(this);

            Broker.Global.Receive<GameResume>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.OnLeftAlone();
                })
                .AddTo(this);
        }

        /// <summary>
        /// ゲームを離れた際の処理
        /// </summary>
        private void OnGameLeft()
        {
            var gameHistory = GameSystem.Instance.User.History.Game;
            if (AutoAntSystem.Advertisement.IsShow)
            {
                gameHistory.GameLeftCase = Constants.GameLeftCase.ByAdvertisement;
            }
            else
            {
                gameHistory.GameLeftCase = Constants.GameLeftCase.Normal;
            }

            gameHistory.LastDateTime = DateTime.Now;
        }

        /// <summary>
        /// 放置されていた時間分の処理を行う
        /// </summary>
        private void OnLeftAlone()
        {
            var user = GameSystem.Instance.User;

            // 広告閲覧によりゲームから離れていたら何もしない
            if(user.History.Game.GameLeftCase == Constants.GameLeftCase.ByAdvertisement)
            {
                return;
            }

            var lastDateTime = user.History.Game.LastDateTime;
            if (DateTime.MinValue == lastDateTime)
            {
                return;
            }

            var oldMoney = user.Wallet.Money;
            var oldPopulation = user.Town.Population.Value;

            var span = Math.Min((DateTime.Now - lastDateTime).TotalSeconds, this.leftAloneProcessSeconds);
            var updatableCount = Math.Floor(span / this.updateInterval);
            for (var i = 0; i < updatableCount; i++)
            {
                this.userUpdater.UpdateParameter(this.updateInterval);
            }

            var diffMoney = user.Wallet.Money - oldMoney;
            var diffPopulation = user.Town.Population.Value - oldPopulation;

            if(diffMoney > 0 || diffPopulation > 0)
            {
                this.CreateLeftAloneResultPopup(diffMoney, diffPopulation);
            }
        }

        /// <summary>
        /// 放置可能な時間後にローカル通知を登録する
        /// </summary>
        private void RegisterLeftAloneLocalNotification()
        {
            AutoAntSystem.LocalNotification.Register(
                this.leftAloneLocalNotificationTitle.Get,
                this.leftAloneLocalNotificationMessage.Get,
                (int)this.leftAloneProcessSeconds
            );
        }

        private void CreateLeftAloneResultPopup(double money, double population)
        {
            var popup = PopupManager.Request(this.popupPrefab);
            popup.Initialize(money, population, this.adsAcquireRate);
            popup.DecideButton.OnClickAsObservable()
                .SubscribeWithState(popup, (_, _popup) =>
                {
                    _popup.Close();
                })
                .AddTo(this);

            var tuple = new Tuple<LeftAloneController, LeftAloneResultPopup, double, double>(
                this,
                popup,
                money,
                population
            );
            popup.AdsButton.OnClickAsObservable()
                .SubscribeWithState(tuple, (_, t) =>
                {
                    var _this = t.Item1;
                    var _popup = t.Item2;
                    var _money = t.Item3;
                    var _population = t.Item4;
                    _this.ShowAds(_popup, _money, _population);
                })
                .AddTo(this);
            popup.Open();
        }

        private void ShowAds(LeftAloneResultPopup popup, double money, double population)
        {
            var tuple = new Tuple<LeftAloneController, LeftAloneResultPopup, double, double>(
                this,
                popup,
                money,
                population
            );
            AutoAntSystem.Advertisement.Show()
                .SubscribeWithState(tuple, (x, t) =>
                {
                    var _this = t.Item1;
                    var _popup = t.Item2;
                    var _money = t.Item3;
                    var _population = t.Item4;

                    if (x == ShowResult.Finished)
                    {
                        var user = GameSystem.Instance.User;

                        // 既に放置分のリソースは加算しているので倍率を補正する
                        var rate = _this.adsAcquireRate - 1;

                        user.Wallet.AddMoney(_money * rate);
                        user.Town.AddPopulation(_population * rate);
                        _popup.Close();
                    }
                })
                .AddTo(this);
        }
    }
}
