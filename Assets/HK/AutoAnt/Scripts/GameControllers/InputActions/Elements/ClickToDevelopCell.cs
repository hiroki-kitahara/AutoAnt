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

        public ClickToDevelopCell(
            CellGenerator cellGenerator,
            CellMapper cellMapper,
            int replaceCellRecordId,
            int blankCellRecordId,
            int generateBlankRange
            )
        {
            this.cellGenerator = cellGenerator;
            this.cellMapper = cellMapper;
            this.replaceCellRecordId = replaceCellRecordId;
            this.blankCellRecordId = blankCellRecordId;
            this.generateBlankRange = generateBlankRange;
        }

        public void Do(InputControllers.Events.ClickData data)
        {
            var gameSystem = GameSystem.Instance;
            var cell = Cell.GetCell(gameSystem.Cameraman.Camera.ScreenPointToRay(data.Position));
            if(cell == null)
            {
                return;
            }

            if(cell.Type != Constants.CellType.Blank)
            {
                return;
            }

            var needMoney = Calculator.DevelopCost(gameSystem.Constants.Cell.DevelopCost, cell.Position);
            if(!gameSystem.User.Wallet.IsEnoughMoney(needMoney))
            {
                return;
            }

            gameSystem.User.Wallet.AddMoney(-needMoney);

            var targets = this.cellMapper.GetCellFromGroup(cell.Group);
            foreach(var c in targets)
            {
                this.cellGenerator.Replace(this.replaceCellRecordId, c.Position);
            }

            var se = gameSystem.Constants.Cell.DevelopSE;
            Assert.IsNotNull(se);
            gameSystem.SEController.Play(se);
        }
    }
}
