using System.ComponentModel;
using HK.AutoAnt.Events;
using HK.AutoAnt.Extensions;
using HK.AutoAnt.Systems;
using HK.AutoAnt.UI;
using UnityEngine;
using UnityEngine.Assertions;
using UniRx;

// #if AA_DEBUG
/// <summary>
/// ゲームに関するデバッグをまとめるクラス
/// </summary>
public partial class SROptions
{
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
        cellManager.Mapper.GetRange(Vector2Int.zero, this.addCellRange, (id) =>
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
        cellManager.Mapper.GetRange(Vector2Int.zero, this.addCellRange, (id) =>
        {
            var cell = cellManager.Mapper.Cell.Map[id];
            if (cellManager.Mapper.CellEvent.Map.ContainsKey(id))
            {
                cellManager.EventGenerator.Erase(cell);
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
        cellManager.Mapper.GetRange(Vector2Int.zero, this.addCellRange, (id) =>
        {
            var cell = cellManager.Mapper.Cell.Map[id];
            if (cellManager.Mapper.CellEvent.Map.ContainsKey(id))
            {
                cellManager.EventGenerator.Erase(cell);
            }

            cellManager.EventGenerator.Generate(cell, facilityId, false);

            return true;
        });
    }


}
// #endif
