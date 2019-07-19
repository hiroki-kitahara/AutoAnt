using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Systems
{
    /// <summary>
    /// ローカル通知を制御するクラス
    /// </summary>
    public sealed class LocalNotifiaction
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        private readonly string DefaultChannelId = "auto_ant";
#elif UNITY_IOS && !UNITY_EDITOR

#endif

        public LocalNotifiaction()
        {
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

        public void Register(string title, string message, string smallIcon, string largeIcon, double delaySeconds)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidNotificationCenter.SendNotification(
                new AndroidNotification
                {
                    Title = title,
                    Text = message,
                    SmallIcon = smallIcon,
                    LargeIcon = largeIcon,
                    FireTime = System.DateTime.Now.AddSeconds(delaySeconds),
                    Number = 1
                },
                DefaultChannelId
            );
#elif UNITY_IOS && !UNITY_EDITOR

#endif
        }
    }
}
