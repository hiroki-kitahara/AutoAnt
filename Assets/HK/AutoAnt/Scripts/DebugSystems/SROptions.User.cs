using System.ComponentModel;
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
}
#endif
