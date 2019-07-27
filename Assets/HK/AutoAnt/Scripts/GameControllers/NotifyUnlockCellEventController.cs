using HK.AutoAnt.Events;
using HK.AutoAnt.Extensions;
using HK.AutoAnt.Systems;
using HK.AutoAnt.UI;
using HK.Framework.EventSystems;
using HK.Framework.Text;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// セルイベントがアンロックされた事をポップアップで通知するクラス
    /// </summary>
    public sealed class NotifyUnlockCellEventController : MonoBehaviour
    {
        [SerializeField]
        private UnlockCellEventPopup popupPrefab = null;

        void Awake()
        {
            Broker.Global.Receive<UnlockedCellEvent>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    var popup = PopupManager.Request(_this.popupPrefab);
                    var record = GameSystem.Instance.MasterData.CellEvent.Records.Get(x.CellEventRecordId);
                    popup.Initialize(record.EventData);
                    popup.Open();
                })
                .AddTo(this);
        }
    }
}
