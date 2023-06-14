using com.eyerunnman.patterns;
namespace com.eyerunnman.HexSweeper.Core.Commands
{
    public class SetHexGridToIdle : ICommand<HexSweeperBehaviour>
    {
        public void Execute(HexSweeperBehaviour context)
        {
            context.SetHexGridToIdle();
        }
    }
}
