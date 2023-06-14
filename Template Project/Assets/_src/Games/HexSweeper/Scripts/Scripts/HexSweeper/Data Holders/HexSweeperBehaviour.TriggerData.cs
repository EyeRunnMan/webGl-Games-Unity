using com.eyerunnman.Helper;
using com.eyerunnman.MnSwpr;
namespace com.eyerunnman.HexSweeper.Core
{
    internal class TriggerData
    {
        public class Reset
        {
            public readonly HexSweeperSettingsContainer HexSweeperSettingsContainer;
            public Reset(HexSweeperSettingsContainer hexSweeperSettingsContainer)
            {
                HexSweeperSettingsContainer = hexSweeperSettingsContainer;
            }
        }
    
        public class RevealCell
        {
            public readonly int CellToReveal;
            public RevealCell(int cellToReveal)
            {
                CellToReveal = cellToReveal;
            }
        }
        public class HighlightCell
        {
            public readonly int CellToReveal;
            public HighlightCell(int cellToReveal)
            {
                CellToReveal = cellToReveal;
            }
        }

        public class MoveHighlightCell
        {
            public readonly Direction moveToDirection;
            public MoveHighlightCell(Direction moveToDirection)
            {
                this.moveToDirection = moveToDirection;
            }
        }
    }
}
