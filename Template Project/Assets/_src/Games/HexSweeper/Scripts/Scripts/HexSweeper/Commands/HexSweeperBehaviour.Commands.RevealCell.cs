using com.eyerunnman.MnSwpr;
using com.eyerunnman.patterns;

namespace com.eyerunnman.HexSweeper.Core.Commands
{
    public class RevealCell : ICommand<HexSweeperBehaviour>
    {
        public void Execute(HexSweeperBehaviour context)
        {
            context.RevealCell();
        }
    }
}
