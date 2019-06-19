using System.Collections.Generic;
using System.Linq;
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
        /// ポップアップのレスポンスが返ってきたらポップアップを閉じる
        /// </summary>
        public static T ResponseToClose<T>(this T self) where T : IPopup
        {
            self.ResponseAsObservable()
                .Take(1)
                .SubscribeWithState(self, (_, _self) =>
                {
                    _self.Close();
                });

            return self;
        }
    }
}
