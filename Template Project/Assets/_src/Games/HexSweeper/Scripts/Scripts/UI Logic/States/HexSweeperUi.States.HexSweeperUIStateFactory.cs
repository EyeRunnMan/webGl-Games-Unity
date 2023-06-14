using com.eyerunnman.patterns;

namespace com.eyerunnman.HexSweeper.Ui.States
{
    internal class UiStateFactory : IFactory<AbstractTriggerableState<HexSweeperUiBehaviour, HexSweeperUIStates, HexSweeperUITriggers>, HexSweeperUIStates>
    {
        HexSweeperUiBehaviour hexSweeperUiBehaviour;
        public UiStateFactory(HexSweeperUiBehaviour hexSweeperUiBehaviour)
        {
            this.hexSweeperUiBehaviour = hexSweeperUiBehaviour;
        }

        public AbstractTriggerableState<HexSweeperUiBehaviour, HexSweeperUIStates, HexSweeperUITriggers> Create(HexSweeperUIStates type)
        {
            return type switch
            {
                HexSweeperUIStates.MainMenu => new MainMenuState(hexSweeperUiBehaviour, type),
                HexSweeperUIStates.GameplayHUD => new GamePlayHUDState(hexSweeperUiBehaviour, type),
                HexSweeperUIStates.Pause => new PauseMenuState(hexSweeperUiBehaviour, type),
                HexSweeperUIStates.Result => new RestultState(hexSweeperUiBehaviour, type),
                _ => null,
            };
        }
    }
}
