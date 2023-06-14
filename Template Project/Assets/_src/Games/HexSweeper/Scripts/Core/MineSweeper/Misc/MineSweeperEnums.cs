namespace com.eyerunnman.MnSwpr
{
    public class MineSweeperEnums
    {
        public enum CellState
        {
            Invalid,
            Hidden,
            Revealed,
        }
        public enum CellType
        {
            Invalid,
            Empty,
            Mine,
            Filled,
        }
        public enum Status
        {
            Invalid,
            Default,
            RevealedAllMines,
            RevealedAllNonMines,
        }
        public enum CellShape
        {
            Square,
            Hexagon
        }
    }
}
