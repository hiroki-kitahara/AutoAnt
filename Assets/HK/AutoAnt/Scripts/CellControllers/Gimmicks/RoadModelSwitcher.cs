using HK.AutoAnt.CellControllers.Events;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers.Gimmicks
{
    /// <summary>
    /// 道路のモデルを切り替えるクラス
    /// </summary>
    public sealed class RoadModelSwitcher : MonoBehaviour, ICellEventGimmick
    {
        public void Attach(CellEvent cellEvent)
        {
        }
    }
}
