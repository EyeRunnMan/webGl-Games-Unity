using com.eyerunnman.patterns;

namespace com.eyerunnman.HexSweeper.Ui.Commands
{
    public class SetupHexSweeperUiStateMachine : ICommand<HexSweeperUiBehaviour>
    {
        readonly InitializeUIBehaviourData hexSweeperSettingsContainer;
        internal SetupHexSweeperUiStateMachine(InitializeUIBehaviourData hexSweeperSettingsContainer)
        {
            this.hexSweeperSettingsContainer = hexSweeperSettingsContainer;
        }

        public void Execute(HexSweeperUiBehaviour context)
        {
            context.InitializeHexSweeperUiViews(hexSweeperSettingsContainer);
        }
    }
}

