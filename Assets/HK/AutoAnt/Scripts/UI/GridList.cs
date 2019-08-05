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

        private GridListElement[] elements = null;

        public void SetData<T>(IEnumerable<T> list, Action<int, T, GridListElement> setAction)
        {
            this.NewElements(list.Count());
            var array = list.ToArray();
            for (var i = 0; i < array.Length; i++)
            {
                var element = this.elementPrefab.Rent();
                this.elements[i] = element;
                element.transform.SetParent(this.elementParent);
                setAction(i, array[i], element);
            }
        }

        public void UpdateData<T>(IEnumerable<T> list, int targetIndex, Action<int, T, GridListElement> updateAction)
        {
            updateAction(targetIndex, list.ToArray()[targetIndex], this.elements[targetIndex]);
        }

        private void NewElements(int length)
        {
            if(this.elements != null)
            {
                foreach (var e in this.elements)
                {
                    e.Return();
                }
            }

            this.elements = new GridListElement[length];
        }
    }
}
