using com.eyerunnman.HexSweeper.Ui.Views;

namespace com.eyerunnman.HexSweeper.Ui
{
    internal class InitializeUIBehaviourData
    {
        MainMenuView mainMenuView;
        PauseMenuView pauseMenuView;
        GameplayHUDView gameplayHUDView;
        ResultMenuView resultMenuView;
        public InitializeUIBehaviourData(MainMenuView mainMenuView, PauseMenuView pauseMenuView, GameplayHUDView gameplayHUDView, ResultMenuView resultMenuView)
        {
            this.mainMenuView = mainMenuView;
            this.pauseMenuView = pauseMenuView;
            this.gameplayHUDView = gameplayHUDView;
            this.resultMenuView = resultMenuView;
        }

        public MainMenuView MainMenuView => mainMenuView;
        public PauseMenuView PauseMenuView => pauseMenuView;
        public GameplayHUDView GameplayHUDView => gameplayHUDView;
        public ResultMenuView ResultMenuView => resultMenuView;

        

        internal void EnableMainMenu()
        {
            mainMenuView.gameObject.SetActive(true);
            pauseMenuView.gameObject.SetActive(false);
            gameplayHUDView.gameObject.SetActive(false);
            resultMenuView.gameObject.SetActive(false);
        }

        internal void EnablePauseMenu()
        {
            mainMenuView.gameObject.SetActive(false);
            pauseMenuView.gameObject.SetActive(true);
            gameplayHUDView.gameObject.SetActive(false);
            resultMenuView.gameObject.SetActive(false);
        }
        internal void EnableGamePlayHUD()
        {
            mainMenuView.gameObject.SetActive(false);
            pauseMenuView.gameObject.SetActive(false);
            gameplayHUDView.gameObject.SetActive(true);
            resultMenuView.gameObject.SetActive(false);
        }
        internal void EnableResultMenu()
        {
            mainMenuView.gameObject.SetActive(false);
            pauseMenuView.gameObject.SetActive(false);
            gameplayHUDView.gameObject.SetActive(false);
            resultMenuView.gameObject.SetActive(true);
        }

    }
}

