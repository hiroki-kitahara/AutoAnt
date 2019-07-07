using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers.Events
{
    /// <summary>
    /// バフの影響を受けることが出来るインターフェイス
    /// </summary>
    public interface IReceiveBuff
    {
        float Buff { get; }

        void AddBuff(float value);
    }
}
