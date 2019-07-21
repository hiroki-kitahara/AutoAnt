using System;
using HK.AutoAnt.Events;
using HK.AutoAnt.Systems;
using HK.Framework.EventSystems;
using HK.Framework.Text;
using UniRx;
using UnityEngine;
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
                    _this.RegisterLeftAloneLocalNotification();
                })
                .AddTo(this);
        }

        /// <summary>
        /// 放置されていた時間分の処理を行う
        /// </summary>
        private void OnLeftAlone()
        {
            var user = GameSystem.Instance.User;
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
                this.userUpdater.UpdateParameter(GameSystem.Instance);
            }

            var newMoney = user.Wallet.Money;
            var newPopulation = user.Town.Population.Value;
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
    }
}
