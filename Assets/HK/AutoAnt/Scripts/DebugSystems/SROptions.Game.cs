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
}
#endif
