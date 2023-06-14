using com.eyerunnman.Helper;
using com.eyerunnman.MnSwpr;
using com.eyerunnman.patterns;

namespace com.eyerunnman.HexSweeper.Core.Commands
{
    public class HighlightCell : ICommand<HexSweeperBehaviour>
    {
        int cellIdToHighlight;
        public HighlightCell(int cellToHighlight)
        {
            this.cellIdToHighlight = cellToHighlight;
        }
        public void Execute(HexSweeperBehaviour context)
        {
            context.HighlightCellTrigger(cellIdToHighlight);
        }
    }
}


namespace com.eyerunnman.HexSweeper.Core.Commands
{
}

