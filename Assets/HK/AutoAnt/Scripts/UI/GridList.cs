using System;
using System.Collections.Generic;
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
        private Transform parent = null;

        public void SetData<T>(List<T> list, Action<int, T, GridListElement> setAction)
        {
            
        }
    }
}
