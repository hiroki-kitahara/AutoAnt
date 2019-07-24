using System.Linq;
using HK.AutoAnt.Extensions;
using HK.AutoAnt.Systems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// フッターメニューの建設ボタンを制御するクラス
    /// </summary>
    public sealed class FooterShowSelectBuildingButtonController : MonoBehaviour
    {
        [SerializeField]
        private Button button = null;

        [SerializeField]
        private FooterController footerController = null;

        [SerializeField]
        private Constants.CellEventCategory showCategory = Constants.CellEventCategory.Housing;

        void Awake()
        {
            this.button.OnClickAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    var records = GameSystem.Instance.User.UnlockCellEvent.Elements
                        .Select(id => GameSystem.Instance.MasterData.CellEvent.Records.Get(id))
                        .Where(r => r.EventData.Category == this.showCategory)
                        .ToArray();

                    _this.footerController.ShowSelectBuilding(records);
                })
                .AddTo(this);
        }
    }
}
