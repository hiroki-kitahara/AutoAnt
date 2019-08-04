using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.UI.Elements
{
    /// <summary>
    /// グリッドリストを制御するクラス
    /// </summary>
    public sealed class GridList : MonoBehaviour
    {
        [SerializeField]
        private GridListElement elementPrefab = null;

        [SerializeField]
        private Transform elementParent = null;

        public void SetData<T>(IEnumerable<T> list, Action<int, T, GridListElement> setAction)
        {
            var array = list.ToArray();
            for (var i = 0; i < array.Length; i++)
            {
                var element = this.elementPrefab.Rent();
                element.transform.SetParent(this.elementParent);
                setAction(i, array[i], element);
            }
        }
    }
}
