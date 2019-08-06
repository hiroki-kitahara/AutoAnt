using System.ComponentModel;
using HK.AutoAnt.Events;
using HK.AutoAnt.Extensions;
using HK.AutoAnt.Systems;
using HK.AutoAnt.UI;
using UnityEngine;
using UnityEngine.Assertions;
using UniRx;
using HK.AutoAnt;
using HK.AutoAnt.SaveData.Internal;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

// #if AA_DEBUG
/// <summary>
/// ゲームに関するデバッグをまとめるクラス
/// </summary>
public partial class SROptions
{
    [Sort(1000)]
    [Category("Game/System")]
    [DisplayName("セーブデータ削除")]
    public void DeleteSaveData()
    {
        SRDebug.Instance.HideDebugPanel();
        var popup = PopupManager.RequestSimplePopup().Initialize("本当に削除しますか？", "OK", "Cancel");
        popup.DecideButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                ES3.DeleteFile();
                SceneManager.LoadScene("Game");
            })
            .AddTo(popup);
        popup.CancelButton.OnClickAsObservable()
            .Subscribe(_ =>
            {
                popup.Close();
            })
            .AddTo(popup);
        popup.Open();
    }

    [Sort(1000)]
    [Category("Game")]
    [DisplayName("3秒後にローカル通知")]
    public void LocalNotificationTest()
    {
        AutoAntSystem.LocalNotification.Register("AutoAnt", "ローカル通知テスト", 3);
    }

    [Sort(1000)]
    [Category("Game")]
    [DisplayName("生成する建設物のID")]
    public int ChangeCellEventGenerator
    {
        get
        {
            return GameSystem.Instance.CellManager.EventGenerator.RecordId;
        }
        set
        {
            GameSystem.Instance.CellManager.EventGenerator.RecordId = value;
        }
    }

    [Sort(1000)]
    [Category("Game")]
    [DisplayName("ポップアップテスト")]
    public void SimplePopupText()
    {
        var popup = PopupManager.RequestSimplePopup()
            .Initialize("やっほー", "OK", "CANCEL");

        popup.DecideButton.OnClickAsObservable()
            .SubscribeWithState(popup, (_, p) => p.Close())
            .AddTo(popup);
            
        popup.CancelButton.OnClickAsObservable()
            .SubscribeWithState(popup, (_, p) => p.Close())
            .AddTo(popup);

        popup.Open();
    }

    private int addCellRange = 5;
    [Sort(1000)]
    [Category("Game/Cell")]
    [DisplayName("追加するセル範囲")]
    public int AddCellRange { get { return this.addCellRange; } set { this.addCellRange = value; } }

    [Sort(1001)]
    [Category("Game/Cell")]
    [DisplayName("セル追加")]
    public void InvokeAddCellRange()
    {
        const int grasslandId = 100100;
        var cellManager = GameSystem.Instance.CellManager;
        Vector2IntUtility.GetRange(Vector2Int.zero, this.addCellRange, (id) =>
        {
            if (cellManager.Mapper.Cell.Map.ContainsKey(id))
            {
                cellManager.CellGenerator.Replace(grasslandId, id);
            }
            else
            {
                cellManager.CellGenerator.Generate(grasslandId, id);
            }

            return true;
        });
    }

    [Sort(1002)]
    [Category("Game/Cell")]
    [DisplayName("住宅追加")]
    public void InvokeAddCellEventHousing()
    {
        const int housingId = 100000;
        var cellManager = GameSystem.Instance.CellManager;
        Vector2IntUtility.GetRange(Vector2Int.zero, this.addCellRange, (id) =>
        {
            var cell = cellManager.Mapper.Cell.Map[id];
            if (cellManager.Mapper.CellEvent.Map.ContainsKey(id))
            {
                cellManager.EventGenerator.Remove(cell);
            }

            cellManager.EventGenerator.Generate(cell, housingId, false);

            return true;
        });
    }

    [Sort(1003)]
    [Category("Game/Cell")]
    [DisplayName("商業追加")]
    public void InvokeAddCellEventFacility()
    {
        const int facilityId = 101000;
        var cellManager = GameSystem.Instance.CellManager;
        Vector2IntUtility.GetRange(Vector2Int.zero, this.addCellRange, (id) =>
        {
            var cell = cellManager.Mapper.Cell.Map[id];
            if (cellManager.Mapper.CellEvent.Map.ContainsKey(id))
            {
                cellManager.EventGenerator.Remove(cell);
            }

            cellManager.EventGenerator.Generate(cell, facilityId, false);

            return true;
        });
    }

    [Sort(1003)]
    [Category("Game/Ads")]
    [DisplayName("広告表示")]
    public void ShowAds()
    {
        AutoAntSystem.Advertisement.Show()
            .Subscribe(x =>
            {
                Debug.Log(x);
            });
    }

    [Sort(1004)]
    [Category("Game")]
    [DisplayName("全てのセルイベントを建設")]
    public void ShowFooterSelectCellEventAll()
    {
        var footerController = GameObject.FindObjectOfType<FooterController>();
        footerController.ShowSelectBuilding(GameSystem.Instance.MasterData.CellEvent.Records);
    }
}
// #endif
