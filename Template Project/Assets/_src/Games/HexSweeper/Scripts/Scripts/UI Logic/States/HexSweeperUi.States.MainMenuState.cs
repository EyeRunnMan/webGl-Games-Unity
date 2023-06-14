using com.eyerunnman.HexSweeper.Core;
using com.eyerunnman.patterns;

namespace com.eyerunnman.HexSweeper.Ui.States
{
    internal class MainMenuState : RootAbstractTriggerableState<HexSweeperUiBehaviour, HexSweeperUIStates, HexSweeperUITriggers>
    {
        public MainMenuState(IAbstractStateMachine<HexSweeperUiBehaviour, HexSweeperUIStates, HexSweeperUITriggers> context, HexSweeperUIStates stateID) 
            : base(context, stateID) { }
        protected override HexSweeperUIStates UndefinedState => HexSweeperUIStates.Undefined;
        
        protected override void OnStateSetup()
        {
            AddTransition(HexSweeperUIStates.GameplayHUD);

        }
        protected override void OnStateAwake()
        {
            Ctx.UiViewData.EnableMainMenu();
            Ctx.UiViewData.MainMenuView.OnPlayButtonClicked += OnPlayClicked;
        }
        protected override void OnStateExit()
        {
            Ctx.UiViewData.MainMenuView.OnPlayButtonClicked += OnPlayClicked;
        }

        private void OnPlayClicked()
        {
            Ctx.HexSweeperController.SetupGame();
            InvokeTransition(HexSweeperUIStates.GameplayHUD);
        }
    }
}
