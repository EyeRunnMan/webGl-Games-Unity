using com.eyerunnman.patterns;

namespace com.eyerunnman.HexSweeper.Core.Commands
{
    public class PauseHexSweeper : ICommand<HexSweeperBehaviour>
    {
        public void Execute(HexSweeperBehaviour context)
        {
            context.PauseStateMachine();
        }
    }
}
