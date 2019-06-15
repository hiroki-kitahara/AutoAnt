using System.ComponentModel;
using System.Linq;
using HK.AutoAnt.Systems;
using UnityEngine;
using UnityEngine.Assertions;

#if AA_DEBUG
/// <summary>
/// ゲームに関するデバッグをまとめるクラス
/// </summary>
public partial class SROptions
{
    private const string UserCategory = "User";

    [Category(UserCategory)]
    [DisplayName("お金")]
    public double SetMoney
    {
        get
        {
            return GameSystem.Instance.User.Wallet.Money;
        }
        set
        {
            GameSystem.Instance.User.Wallet.SetMoney(value);
        }
    }

    [Category(UserCategory)]
    [DisplayName("全部のアイテム手に入れる")]
    public void AcquireItemAll()
    {
        var masterData = GameSystem.Instance.MasterData.Item;
        foreach(var record in masterData.Records)
        {
            GameSystem.Instance.User.Inventory.AddItem(record, 10);
        }
    }

    [Category(UserCategory)]
    [DisplayName("建設履歴を表示する")]
    public void PrintGenerateCellEventHistories()
    {
        foreach (var h in GameSystem.Instance.User.History.GenerateCellEvent)
        {
            Debug.Log($"CellEventRecordId = {h.Key}, numbers = {string.Join(",", h.Value.Numbers.Select(n => n.ToString()))}");
        }
    }

    [Category(UserCategory)]
    [DisplayName("アンロックを表示する")]
    public void PrintUnlockCellEvent()
    {
        Debug.Log($"{string.Join(",", GameSystem.Instance.User.UnlockCellEvents.CellEvents.Select(x => x.ToString()))}");
    }
}
#endif
