using HK.AutoAnt.Database;
using HK.AutoAnt.UserControllers;
using HK.Framework.EventSystems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.Events
{
    /// <summary>
    /// セルイベント生成の履歴が追加された際のイベント
    /// </summary>
    public sealed class AddedGenerateCellEventHistory : Message<AddedGenerateCellEventHistory, GenerateCellEventHistory, int>
    {
        /// <summary>
        /// 追加された<see cref="GenerateCellEventHistory"/>
        /// </summary>
        public GenerateCellEventHistory GenerateCellEventHistory => this.param1;

        /// <summary>
        /// 追加されたセルイベントのレコードID
        /// </summary>
        public int CellEventRecordId => this.param2;
    }
}
