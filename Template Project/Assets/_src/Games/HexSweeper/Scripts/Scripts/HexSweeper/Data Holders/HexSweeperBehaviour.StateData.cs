using com.eyerunnman.MnSwpr;
namespace com.eyerunnman.HexSweeper.Core
{
    internal class StateData
    {
        public class RevealCell
        {
            public int CellToReveal;
            public RevealCell(int cellToReveal)
            {
                CellToReveal = cellToReveal;
            }
        }
    
        public class HighlightCell
        {
            public int cellToHighlight;
            public HighlightCell(int cellToHighlight)
            {
                this.cellToHighlight = cellToHighlight;
            }
        }
    
        public class Reset
        {
            public HexSweeperSettingsContainer HexSweeperSettingsContainer;
            public Reset(HexSweeperSettingsContainer hexSweeperSettingsContainer)
            {
                HexSweeperSettingsContainer = hexSweeperSettingsContainer;
            }
        }
    }
}
