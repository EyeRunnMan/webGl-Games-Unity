using com.eyerunnman.patterns;
namespace com.eyerunnman.HexSweeper.Core.Commands
{
    public class SetupGame : ICommand<HexSweeperBehaviour>
    {
        readonly HexSweeperSettingsContainer hexSweeperSettingsContainer;
        public SetupGame(HexSweeperSettingsContainer hexSweeperSettingsContainer)
        {
            this.hexSweeperSettingsContainer = hexSweeperSettingsContainer;
        }
        public void Execute(HexSweeperBehaviour context)
        {
            context.ResetGame(hexSweeperSettingsContainer);
        }
    }
}
