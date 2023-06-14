using com.eyerunnman.HexSweeper.Core;
using com.eyerunnman.patterns;
using System.Threading.Tasks;

namespace com.eyerunnman.HexSweeper.Ui.States
{
    internal class GamePlayHUDState : RootAbstractTriggerableState<HexSweeperUiBehaviour, HexSweeperUIStates, HexSweeperUITriggers>
    {

        public GamePlayHUDState(IAbstractStateMachine<HexSweeperUiBehaviour, HexSweeperUIStates, HexSweeperUITriggers> context, HexSweeperUIStates stateID)
            : base(context, stateID) { }
        protected override HexSweeperUIStates UndefinedState => HexSweeperUIStates.Undefined;

        protected override void OnStateSetup()
        {
            AddTransition(HexSweeperUIStates.Pause);
            AddTransition(HexSweeperUIStates.Result);


        }
        protected override void OnStateAwake()
        {
            NewMethod();

            async void NewMethod()
            {
                Ctx.UiViewData.EnableGamePlayHUD();
                await Task.Yield();
                Ctx.UiViewData.GameplayHUDView.SetNumberOfCellsFlaggedText((Ctx.HexSweeperProxy.TotalMineCount - Ctx.HexSweeperProxy.TotalFlaggedCount).ToString());

                Ctx.UiViewData.GameplayHUDView.OnPauseButtonClicked += OnPauseButtonClicked;
                Ctx.UiViewData.GameplayHUDView.OnSoundToggle += OnSoundToggle;
                Ctx.HexSweeperProxy.OnWin += OnWin;
                Ctx.HexSweeperProxy.OnLose += OnLose;
                Ctx.HexSweeperProxy.OnCellFlagged += OnCellFlagged;
            }
        }
        protected override void OnStateExit()
        {
            Ctx.UiViewData.GameplayHUDView.OnPauseButtonClicked -= OnPauseButtonClicked;
            Ctx.UiViewData.GameplayHUDView.OnSoundToggle -= OnSoundToggle;
            Ctx.HexSweeperProxy.OnWin -= OnWin;
            Ctx.HexSweeperProxy.OnLose -= OnLose;
            Ctx.HexSweeperProxy.OnCellFlagged -= OnCellFlagged;
        }

        private void OnResult(bool isWin)
        {
            Ctx.IsWin = isWin;
            InvokeTransition(HexSweeperUIStates.Result);
        }
        private void OnWin()
        {
            OnResult(true);
        }
        private void OnLose()
        {
            OnResult(false);
        }

        private void OnCellFlagged(int number)
        {
            int minesCount = Ctx.HexSweeperProxy.TotalMineCount;

            Ctx.UiViewData.GameplayHUDView.SetNumberOfCellsFlaggedText((minesCount - number).ToString());
        }

        private void OnPauseButtonClicked()
        {
            InvokeTransition(HexSweeperUIStates.Pause);
            Ctx.HexSweeperController.PauseGame();
        }
        private void OnSoundToggle(bool status)
        {
            Ctx.AudioManager.ToggleSound(!status);
        }
    }
}
