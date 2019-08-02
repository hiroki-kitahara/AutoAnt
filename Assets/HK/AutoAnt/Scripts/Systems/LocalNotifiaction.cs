using UnityEngine;
using UnityEngine.Assertions;

#if UNITY_ANDROID && !UNITY_EDITOR
using Unity.Notifications.Android;
#elif UNITY_IOS && !UNITY_EDITOR
using Unity.Notifications.iOS;
#endif

namespace HK.AutoAnt.Systems
{
    /// <summary>
    /// ローカル通知を制御するクラス
    /// </summary>
    public sealed class LocalNotifiaction
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        private readonly string DefaultChannelId = "auto_ant";

        private readonly string DefaultSmallIcon = "small_icon_0";

        private readonly string DefaultLargeIcon = "large_icon_0";
#elif UNITY_IOS && !UNITY_EDITOR

#endif

        private static bool isInitialized = false;

        public LocalNotifiaction()
        {
            if(isInitialized)
            {
                Assert.IsTrue(false, $"既に{typeof(LocalNotifiaction).Name}は初期化済みです");
                return;
            }
            
            isInitialized = true;

#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidNotificationCenter.RegisterNotificationChannel(new AndroidNotificationChannel
            {
                Id = DefaultChannelId,
                Name = "AutoAnt",
                Importance = Importance.Default,
                Description = "AutoAntの通知チャンネル説明"
            });
#elif UNITY_IOS && !UNITY_EDITOR

#endif
        }

        public void Register(string title, string message, int afterSeconds)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            this.Register(title, message, afterSeconds, DefaultSmallIcon, DefaultLargeIcon);
#elif UNITY_IOS && !UNITY_EDITOR
            this.Register(title, message, afterSeconds, "", "");
#endif
        }

        public void Register(string title, string message, int afterSeconds, string smallIcon, string largeIcon)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidNotificationCenter.SendNotification(
                new AndroidNotification
                {
                    Title = title,
                    Text = message,
                    SmallIcon = smallIcon,
                    LargeIcon = largeIcon,
                    FireTime = System.DateTime.Now.AddSeconds(afterSeconds),
                    Number = 1
                },
                DefaultChannelId
            );
#elif UNITY_IOS && !UNITY_EDITOR
            iOSNotificationCenter.ScheduleNotification(new iOSNotification
            {
                Title = title,
                Body = message,
                ShowInForeground = true,
                Badge = 1,
                Trigger = new iOSNotificationTimeIntervalTrigger
                {
                    TimeInterval = new System.TimeSpan(0, 0, afterSeconds),
                    Repeats = false
                }
            });
#endif
        }

        /// <summary>
        /// バッジと登録しているローカル通知を削除する
        /// </summary>
        public void Clear()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            // Androidにバッジ処理が無いっぽい？
            AndroidNotificationCenter.CancelAllNotifications();
#elif UNITY_IOS && !UNITY_EDITOR
            iOSNotificationCenter.ApplicationBadge = 0;
            iOSNotificationCenter.RemoveAllScheduledNotifications();
#endif
        }
    }
}
