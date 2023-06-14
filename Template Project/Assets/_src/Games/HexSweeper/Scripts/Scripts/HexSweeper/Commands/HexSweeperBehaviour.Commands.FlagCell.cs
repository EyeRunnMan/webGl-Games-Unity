using com.eyerunnman.patterns;


namespace com.eyerunnman.HexSweeper.Core.Commands
{
    public class FlagCell : ICommand<HexSweeperBehaviour>
    {
        public void Execute(HexSweeperBehaviour context)
        {
            context.FlagCellTrigger();
        }
    }
}

