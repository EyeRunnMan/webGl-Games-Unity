namespace com.eyerunnman.MnSwpr
{
    public interface IMineSweeperCell
    {
        protected internal virtual void OnCellDataSetup(MineSweeperCellData updatedCellData, MineSweeper mineSweeper) { }
        protected internal virtual void OnCellDataUpdate(MineSweeperCellData updatedCellData) { }
    }
}

