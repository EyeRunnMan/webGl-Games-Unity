using com.eyerunnman.HexSweeper.Ui.Commands;
using com.eyerunnman.HexSweeper.Ui.Views;
using com.eyerunnman.patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.eyerunnman.HexSweeper.Ui
{
    [RequireComponent(typeof(HexSweeperUiBehaviourProxy))]
    public class HexSweeperUiProxyController : MonoBehaviour
    {
        [SerializeField]
        MainMenuView mainMenuView;
        [SerializeField]
        PauseMenuView pauseMenuView;
        [SerializeField]
        GameplayHUDView gameplayHUDView;
        [SerializeField]
        ResultMenuView resultMenuView;

        HexSweeperUiBehaviourProxy hexSweeperUiBehaviourProxy;

        private void Awake()
        {
            hexSweeperUiBehaviourProxy = GetComponent<HexSweeperUiBehaviourProxy>();
            SetupGame();

        }
        private void Start()
        {
        }
        private void SetupGame()
        {
            InitializeUIBehaviourData data = new(mainMenuView, pauseMenuView, gameplayHUDView, resultMenuView);

            ICommand<HexSweeperUiBehaviour> setupUI = new SetupHexSweeperUiStateMachine(data);

            hexSweeperUiBehaviourProxy.ExecuteCommand(setupUI);

        }
    }
}

