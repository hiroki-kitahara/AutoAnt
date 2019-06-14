using HK.AutoAnt.CellControllers.Gimmicks;
using HK.AutoAnt.Systems;
using UnityEngine;
using UnityEngine.Assertions;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HK.AutoAnt.CellControllers.Events
{
    /// <summary>
    /// <see cref="Cell"/>のイベントを持つ抽象クラス
    /// </summary>
    /// <remarks>
    /// 何もしないギミックを持つイベント特化のクラス
    /// ギミックになにか仕込みたい場合は<see cref="CellEvent"/>を継承してください
    /// </remarks>
    public abstract class CellEventBlankGimmick : CellEvent
    {
        [SerializeField]
        protected Blank gimmickPrefab = null;

        public override CellGimmickController CreateGimmickController(Vector2Int origin)
        {
            var gimmick = Instantiate(this.gimmickPrefab);
            var constants = GameSystem.Instance.Constants.Cell;
            var position = new Vector3(origin.x * (constants.Scale.x + constants.Interval), 0.0f, origin.y * (constants.Scale.z + constants.Interval));
            var fixedSize = this.size - 1;
            position += new Vector3((constants.Scale.x / 2.0f) * fixedSize, 0.0f, (constants.Scale.z / 2.0f) * fixedSize);
            position += new Vector3(constants.Interval * fixedSize, constants.Scale.y, constants.Interval * fixedSize);
            gimmick.transform.position = position;
            gimmick.transform.localScale = constants.EffectScale * this.size + (Vector3.one * (constants.Interval * fixedSize));

            return gimmick;
        }

#if UNITY_EDITOR
        protected override void ApplyProperty(Database.SpreadSheetData.CellEventData data)
        {
            base.ApplyProperty(data);
            this.gimmickPrefab = AssetDatabase.LoadAssetAtPath<GameObject>($"Assets/HK/AutoAnt/Prefabs/CellEvent/{data.Gimmickprefab}.prefab").GetComponent<Blank>();
        }
#endif
    }
}
