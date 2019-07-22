using HK.AutoAnt.Events;
using HK.AutoAnt.Systems;
using HK.AutoAnt.UI;
using HK.Framework.EventSystems;
using HK.Framework.Text;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// オプションポップアップを制御するクラス
    /// </summary>
    public sealed class OptionPopupController : MonoBehaviour
    {
        [SerializeField]
        private OptionPopup optionPopupPrefab = null;

        /// <summary>
        /// セーブデータを削除するか確認するメッセージ
        /// </summary>
        [SerializeField]
        private StringAsset.Finder confirmDeleteSaveDataMessage = null;

        /// <summary>
        /// 本当にセーブデータを削除するか確認するメッセージ
        /// </summary>
        [SerializeField]
        private StringAsset.Finder trulyConfirmDeleteSaveDataMessage = null;

        [SerializeField]
        private StringAsset.Finder okMessage = null;

        [SerializeField]
        private StringAsset.Finder cancelMessage = null;

        void Awake()
        {
            Broker.Global.Receive<RequestOpenOptionPopup>()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.CreateOptionPopup();
                })
                .AddTo(this);
        }

        private void CreateOptionPopup()
        {
            var popup = PopupManager.Request(this.optionPopupPrefab);

            popup.BGMSlider.value = GameSystem.Instance.User.Option.BGMVolume;
            popup.SESlider.value = GameSystem.Instance.User.Option.SEVolume;

            popup.BGMSlider.OnValueChangedAsObservable()
                .Subscribe(x =>
                {
                    GameSystem.Instance.User.Option.BGMVolume = x;
                })
                .AddTo(this);

            popup.SESlider.OnValueChangedAsObservable()
                .Subscribe(x =>
                {
                    GameSystem.Instance.User.Option.SEVolume = x;
                })
                .AddTo(this);

            popup.DeleteSaveDataButton.OnClickAsObservable()
                .SubscribeWithState(this, (_, _this) =>
                {
                    _this.ConfirmDeleteSaveData();
                })
                .AddTo(this);

            popup.CloseButton.OnClickAsObservable()
                .SubscribeWithState(popup, (_, _popup) =>
                {
                    _popup.Close();
                })
                .AddTo(this);

            popup.Open();
        }

        private void ConfirmDeleteSaveData()
        {
            var popup = PopupManager.RequestSimplePopup()
                .Initialize(this.confirmDeleteSaveDataMessage.Get, this.okMessage.Get, this.cancelMessage.Get);

            popup.DecideButton.OnClickAsObservable()
                .SubscribeWithState2(this, popup, (_, _this, _popup) =>
                {
                    _this.TrulyConfirmDeleteSaveData();
                    _popup.Close();
                })
                .AddTo(this);

            popup.CancelButton.OnClickAsObservable()
                .SubscribeWithState2(this, popup, (_, _this, _popup) =>
                {
                    _popup.Close();
                })
                .AddTo(this);

            popup.Open();
        }

        private void TrulyConfirmDeleteSaveData()
        {
            var popup = PopupManager.RequestSimplePopup()
                .Initialize(this.trulyConfirmDeleteSaveDataMessage.Get, this.okMessage.Get, this.cancelMessage.Get);

            popup.DecideButton.OnClickAsObservable()
                .SubscribeWithState2(this, popup, (_, _this, _popup) =>
                {
                    _this.InvokeDeleteSaveData();
                    _popup.Close();
                })
                .AddTo(this);

            popup.CancelButton.OnClickAsObservable()
                .SubscribeWithState2(this, popup, (_, _this, _popup) =>
                {
                    _popup.Close();
                })
                .AddTo(this);

            popup.Open();
        }

        private void InvokeDeleteSaveData()
        {
            ES3.DeleteFile();
            SceneManager.LoadScene("Game");
        }
    }
}
