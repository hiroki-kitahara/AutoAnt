using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt
{
    /// <summary>
    /// セルの中枢となるクラス
    /// </summary>
    public sealed class Cell : MonoBehaviour, IClickableObject
    {
        public void OnClickDown()
        {
            Debug.Log($"{this.name} OnClickDown", this);
        }

        public void OnClickUp()
        {
            Debug.Log($"{this.name} OnClickUp", this);
        }
    }
}
