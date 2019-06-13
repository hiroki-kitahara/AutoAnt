using HK.AutoAnt.CameraControllers;
using HK.AutoAnt.CellControllers;
using HK.AutoAnt.Systems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// セルの開拓をするアクション
    /// </summary>
    public sealed class DevelopCellActions : InputActions
    {
        public DevelopCellActions(
            GameSystem gameSystem,
            CellGenerator cellGenerator,
            CellMapper cellMapper,
            int replaceCellRecordId,
            int blankCellRecordId,
            int generateBlankRange,
            GameCameraController gameCameraController
            )
            : base(
                null,
                null,
                new ClickToDevelopCell(
                    gameSystem,
                    cellGenerator,
                    cellMapper,
                    replaceCellRecordId,
                    blankCellRecordId,
                    generateBlankRange
                    ),
                new DragToMoveCamera(gameCameraController),
                null
            )
        {
        }
    }
}
