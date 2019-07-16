using HK.AutoAnt.Database;
using HK.AutoAnt.Events;
using HK.AutoAnt.UserControllers;
using HK.Framework.EventSystems;
using HK.Framework.Text;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UI
{
    /// <summary>
    /// 通知UIを制御するクラス
    /// </summary>
    public sealed class NotificationUIController : MonoBehaviour
    {
        [SerializeField]
        private NotificationUIElement elementPrefab = null;

        [SerializeField]
        private float delayElementDestroy = 0.0f;

        /// <summary>
        /// アイテムを取得した時のメッセージフォーマット
        /// </summary>
        [SerializeField]
        private StringAsset.Finder acquireItemFormat = null;

        /// <summary>
        /// セルが無い時にセルイベントを生成しようとした時のメッセージフォーマット
        /// </summary>
        [SerializeField]
        private StringAsset.Finder notCell = null;

        /// <summary>
        /// 既にセルイベントが存在するのにセルイベントを生成しようとした時のメッセージフォーマット
        /// </summary>
        [SerializeField]
        private StringAsset.Finder alreadyExistsCellEventFormat = null;

        /// <summary>
        /// コストが足りないのにセルイベントを生成しようとした時のメッセージフォーマット
        /// </summary>
        [SerializeField]
        private StringAsset.Finder notEnoughCost = null;

        /// <summary>
        /// セルイベントごとの条件を満たしていないのにセルイベントを生成しようとした時のメッセージフォーマット
        /// </summary>
        [SerializeField]
        private StringAsset.Finder notEnoughCondition = null;

        void Awake()
        {
            Broker.Global.Receive<AddedItem>()
                .Where(x => x.Amount > 0)
                .SubscribeWithState(this, (x, _this) =>
                {
                    var message = _this.acquireItemFormat.Format(x.Item.Name, x.Amount, x.Inventory.Items[x.Item.Id]);
                    _this.CreateElement(message, NotificationUIElement.MessageType.Information);
                })
                .AddTo(this);

            Broker.Global.Receive<RequestNotification>()
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.CreateElement(x.Message, x.MessageType);
                })
                .AddTo(this);

            Broker.Global.Receive<ProcessedGenerateCellEvent>()
                .Where(x => x.Evalute != Constants.CellEventGenerateEvalute.Possible)
                .SubscribeWithState(this, (x, _this) =>
                {
                    _this.CreateElement(_this.GetProcessedCellEventMessage(x.Evalute), NotificationUIElement.MessageType.Error);
                })
                .AddTo(this);
        }

        private void CreateElement(string message, NotificationUIElement.MessageType messageType)
        {
            var element = this.elementPrefab.Rent(this.transform, message, messageType, this.delayElementDestroy);
        }

        private string GetProcessedCellEventMessage(Constants.CellEventGenerateEvalute evalute)
        {
            switch(evalute)
            {
                case Constants.CellEventGenerateEvalute.NotCell:
                    return this.notCell.Get;
                case Constants.CellEventGenerateEvalute.AlreadyExistsCellEvent:
                    return this.alreadyExistsCellEventFormat.Get;
                case Constants.CellEventGenerateEvalute.NotEnoughCost:
                    return this.notEnoughCost.Get;
                case Constants.CellEventGenerateEvalute.NotEnoughCondition:
                    return this.notEnoughCondition.Get;
                default:
                    Assert.IsTrue(false, $"{evalute}は未対応です");
                    return null;
            }
        }
    }
}
