using com.eyerunnman.patterns;

namespace com.eyerunnman.HexSweeper.Ui.States
{
    internal class PauseMenuState : RootAbstractTriggerableState<HexSweeperUiBehaviour, HexSweeperUIStates, HexSweeperUITriggers>
    {
        public PauseMenuState(IAbstractStateMachine<HexSweeperUiBehaviour, HexSweeperUIStates, HexSweeperUITriggers> context, HexSweeperUIStates stateID)
            : base(context, stateID) { }
        protected override HexSweeperUIStates UndefinedState => HexSweeperUIStates.Undefined;

        protected override void OnStateSetup()
        {
            AddTransition(HexSweeperUIStates.GameplayHUD);
            AddTransition(HexSweeperUIStates.MainMenu);


        }
        protected override void OnStateAwake()
        {
            Ctx.UiViewData.EnablePauseMenu();
            Ctx.UiViewData.PauseMenuView.OnResumeButtonClicked += OnResumeClicked;
            Ctx.UiViewData.PauseMenuView.OnMainMenuButtonClicked += OnMainMenuButtonClicked;
        }
        protected override void OnStateExit()
        {
            Ctx.UiViewData.PauseMenuView.OnResumeButtonClicked -= OnResumeClicked;
            Ctx.UiViewData.PauseMenuView.OnMainMenuButtonClicked -= OnMainMenuButtonClicked;
        }

        private void OnResumeClicked()
        {
            Ctx.HexSweeperController.ResumeGame();
            InvokeTransition(HexSweeperUIStates.GameplayHUD);
        }
        private void OnMainMenuButtonClicked()
        {
            Ctx.HexSweeperController.ResumeGame();
            Ctx.HexSweeperController.SetGameToIdle();
            InvokeTransition(HexSweeperUIStates.MainMenu);
        }
    }
}
