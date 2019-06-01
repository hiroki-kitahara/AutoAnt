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
        void Initialize(Vector2Int position, GameSystem gameSystem);

        /// <summary>
        /// 削除処理
        /// </summary>
        void Remove();

        /// <summary>
        /// <see cref="CellGimmickController"/>を生成する
        /// </summary>
        CellGimmickController CreateGimmickController();

        /// <summary>
        /// 作成可能か返す
        /// </summary>
        bool CanGenerate(Cell owner, CellMapper cellMapper);

        /// <summary>
        /// セルがクリックされた時の処理
        /// </summary>
        void OnClick(Cell owner);
    }
}
