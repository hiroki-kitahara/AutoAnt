using HK.AutoAnt.CellControllers.Gimmicks;
using HK.AutoAnt.Systems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.CellControllers.Events
{
    /// <summary>
    /// <see cref="Cell"/>の各種イベントを持つインターフェイス
    /// </summary>
    public interface ICellEvent
    {
        /// <summary>
        /// 原点座標
        /// </summary>
        Vector2Int Origin { get; }

        /// <summary>
        /// セルサイズ
        /// </summary>
        /// <remarks>
        /// 正方形にのみ対応しています
        /// </remarks>
        int Size { get; }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <remarks>
        /// <see cref="isInitializeGame"/>はセーブデータから生成された場合は<c>true</c>になります
        /// 例えばセーブデータから復帰した場合はSEを再生しないなどに利用してください
        /// </remarks>
        void Initialize(Vector2Int position, GameSystem gameSystem, bool isInitializeGame);

        /// <summary>
        /// 削除処理
        /// </summary>
        void Remove(GameSystem gameSystem);

        /// <summary>
        /// <see cref="CellGimmickController"/>を生成する
        /// </summary>
        CellGimmickController CreateGimmickController(Vector2Int origin);

        /// <summary>
        /// 作成可能か返す
        /// </summary>
        bool CanGenerate(Cell owner, int cellEventRecordId, GameSystem gameSystem, CellMapper cellMapper);

        /// <summary>
        /// セルがクリックされた時の処理
        /// </summary>
        void OnClick(Cell owner);
    }
}
