using System.Collections.Generic;
using System.Linq;
using HK.AutoAnt.Events;
using HK.AutoAnt.UI;
using UniRx;

namespace HK.AutoAnt.Extensions
{
    /// <summary>
    /// <see cref="IPopup"/>に関する拡張関数
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// レスポンスが返ってきたらポップアップを閉じる
        /// </summary>
        public static T ResponseToClose<T>(this T self) where T : IPopup
        {
            self.Broker.Receive<PopupEvents.Response>()
                .TakeUntil(self.Broker.Receive<PopupEvents.CompleteClose>())
                .SubscribeWithState(self, (_, _self) =>
                {
                    _self.Close();
                });

            return self;
        }
    }
}
