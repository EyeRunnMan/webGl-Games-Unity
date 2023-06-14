using com.eyerunnman.patterns;

namespace com.eyerunnman.HexSweeper.Core.Commands
{
    public class ResumeHexSweeper : ICommand<HexSweeperBehaviour>
    {
        public void Execute(HexSweeperBehaviour context)
        {
            context.ResumeStateMachine();
        }
    }
}
