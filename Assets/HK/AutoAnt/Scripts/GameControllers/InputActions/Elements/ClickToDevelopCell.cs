using HK.AutoAnt.CellControllers;
using HK.AutoAnt.InputControllers;
using HK.AutoAnt.Systems;
using UnityEngine;
using UnityEngine.Assertions;

namespace HK.AutoAnt.GameControllers
{
    /// <summary>
    /// クリック操作によるセル開拓を行うアクション
    /// </summary>
    /// <remarks>
    /// Blankセルを置き換えたあとに周りにセルが無い場合はBlankセルを作成する
    /// </remarks>
    public sealed class ClickToDevelopCell : IInputAction<InputControllers.Events.ClickData>
    {
        private readonly CellGenerator cellGenerator;

        private readonly CellMapper cellMapper;

        // FIXME: 置き換えるIDも算出出来るようにしたほうがいいか？
        private readonly int replaceCellRecordId;

        private readonly int blankCellRecordId;

        private int generateBlankRange;

        public ClickToDevelopCell(CellGenerator cellGenerator, CellMapper cellMapper, int replaceCellRecordId, int blankCellRecordId, int generateBlankRange)
        {
            this.cellGenerator = cellGenerator;
            this.cellMapper = cellMapper;
            this.replaceCellRecordId = replaceCellRecordId;
            this.blankCellRecordId = blankCellRecordId;
            this.generateBlankRange = generateBlankRange;
        }

        public void Do(InputControllers.Events.ClickData data)
        {
            var cell = CellManager.GetCell(GameSystem.Instance.Cameraman.Camera.ScreenPointToRay(data.Position));
            if(cell == null)
            {
                return;
            }

            this.cellGenerator.Replace(this.replaceCellRecordId, cell.Position, null);

            // 周りのセルのない座標にBlankセルを作成する　
            foreach(var position in this.cellMapper.GetEmptyPositions(cell.Position, this.generateBlankRange))
            {
                this.cellGenerator.Generate(this.blankCellRecordId, position, null);
            }
        }
    }
}
