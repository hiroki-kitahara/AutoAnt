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
        private readonly GameSystem gameSystem;

        private readonly CellGenerator cellGenerator;

        private readonly CellMapper cellMapper;

        // FIXME: 置き換えるIDも算出出来るようにしたほうがいいか？
        private readonly int replaceCellRecordId;

        private readonly int blankCellRecordId;

        private int generateBlankRange;

        public ClickToDevelopCell(
            GameSystem gameSystem,
            CellGenerator cellGenerator,
            CellMapper cellMapper,
            int replaceCellRecordId,
            int blankCellRecordId,
            int generateBlankRange
            )
        {
            this.gameSystem = gameSystem;
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

            if(cell.Type != Constants.CellType.Blank)
            {
                return;
            }

            var needMoney = Calculator.DevelopCost(this.gameSystem, cell.Position);
            if(!this.gameSystem.User.Wallet.IsEnoughMoney(needMoney))
            {
                return;
            }

            this.gameSystem.User.Wallet.AddMoney(-needMoney);
            this.cellGenerator.Replace(this.replaceCellRecordId, cell.Position);

            // 周りのセルのない座標にBlankセルを作成する　
            foreach(var position in this.cellMapper.GetEmptyPositions(cell.Position, this.generateBlankRange))
            {
                this.cellGenerator.Generate(this.blankCellRecordId, position);
            }

            var se = this.gameSystem.Constants.Cell.DevelopSE;
            Assert.IsNotNull(se);
            GameSystem.Instance.SEController.Play(se);
        }
    }
}
