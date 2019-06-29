using HK.AutoAnt.CellControllers.Events;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers.Gimmicks
{
    /// <summary>
    /// セルイベントの3D空間上に存在するオブジェクトで各イベントに対して色々処理を行うインターフェイス
    /// </summary>
    public interface ICellEventGimmick
    {
        /// <summary>
        /// 自分自身がセルイベントにアタッチされた際の処理
        /// </summary>
        void Attach(CellEvent cellEvent);
    }
}
