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
    public int SetMoney
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
}
#endif
