using com.eyerunnman.patterns;

namespace com.eyerunnman.HexSweeper.Ui.States
{
    internal class RestultState : RootAbstractTriggerableState<HexSweeperUiBehaviour, HexSweeperUIStates, HexSweeperUITriggers>
    {
        const string WinText = "You Won";
        const string LostText = "GameOver";

        public RestultState(IAbstractStateMachine<HexSweeperUiBehaviour, HexSweeperUIStates, HexSweeperUITriggers> context, HexSweeperUIStates stateID)
            : base(context, stateID) { }
        protected override HexSweeperUIStates UndefinedState => HexSweeperUIStates.Undefined;


        protected override void OnStateSetup()
        {
            AddTransition(HexSweeperUIStates.GameplayHUD);
            AddTransition(HexSweeperUIStates.MainMenu);


        }
        protected override void OnStateAwake()
        {
            Ctx.UiViewData.EnableResultMenu();
            Ctx.UiViewData.ResultMenuView.SetResultText(Ctx.IsWin ? WinText : LostText);

            Ctx.UiViewData.ResultMenuView.OnMainMenuButtonClicked += OnMainMenuButtonClicked;
            Ctx.UiViewData.ResultMenuView.OnReplayButtonClicked += OnReplayButtonClicked;
        }
        protected override void OnStateExit()
        {
            Ctx.UiViewData.ResultMenuView.OnMainMenuButtonClicked -= OnMainMenuButtonClicked;
            Ctx.UiViewData.ResultMenuView.OnReplayButtonClicked -= OnReplayButtonClicked;
        }

        private void OnMainMenuButtonClicked()
        {
            Ctx.HexSweeperController.SetGameToIdle();
            InvokeTransition(HexSweeperUIStates.MainMenu);
        }

        private void OnReplayButtonClicked()
        {
            Ctx.HexSweeperController.SetupGame();
            InvokeTransition(HexSweeperUIStates.GameplayHUD);
        }
    }
}
