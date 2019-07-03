using HK.AutoAnt.CellControllers.Events;
using HK.AutoAnt.Events;
using HK.AutoAnt.Systems;
using HK.AutoAnt.UI;
using HK.Framework.EventSystems;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// <see cref="CellEventDetailsPopup"/>に様々な処理を仕込むクラス
    /// </summary>
    public sealed class CellEventDetailsPopupController : MonoBehaviour
    {
        [SerializeField]
        private CellEventDetailsPopup popup;
        
        void Awake()
        {
            Broker.Global.Receive<RequestOpenCellEventDetailsPopup>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.CreatePopup(x.CellEvent);
                })
                .AddTo(this);
        }

        private void CreatePopup(CellEvent cellEvent)
        {
            var popup = PopupManager.Request(this.popup);
            popup.Initialize(cellEvent);

            // レベルアップボタンの適用
            var levelUpEvent = cellEvent as ILevelUpEvent;
            var existsLevelUpEvent = levelUpEvent != null;
            popup.SetActiveLevelUpButton(existsLevelUpEvent);
            if(existsLevelUpEvent)
            {
                popup.LevelUpButton.OnClickAsObservable()
                    .Where(_ => levelUpEvent.CanLevelUp())
                    .SubscribeWithState2(popup, levelUpEvent, (_, p, _levelUpEvelt) =>
                    {
                        _levelUpEvelt.LevelUp();
                        popup.UpdateProperties();
                    })
                    .AddTo(popup);
            }

            // 解体ボタンの適用
            popup.RemoveButton.OnClickAsObservable()
                .SubscribeWithState2(popup, cellEvent, (_, p, _cellEvent) =>
                {
                    var cellManager = GameSystem.Instance.CellManager;
                    cellManager.EventGenerator.Erase(_cellEvent);
                    p.Close();
                })
                .AddTo(popup);

            // MEMO: ポップアップが閉じる条件
            // 閉じるボタンが押されたとき
            // カメラのドラッグ操作が開始したとき
            // 他のポップアップが開いたとき
            Observable.Merge(
                popup.CloseButton.OnClickAsObservable(),
                InputControllers.Input.Current.DragAsObservable().AsUnitObservable(),
                Broker.Global.Receive<PopupEvents.StartOpen>().Where(x => x.Popup != popup).AsUnitObservable()
            )
                .SubscribeWithState(popup, (_, p) =>
                {
                    p.Close();
                })
                .AddTo(popup);

            popup.Open();
        }
    }
}
