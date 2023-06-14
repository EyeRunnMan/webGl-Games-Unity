using com.eyerunnman.Helper;
using com.eyerunnman.HexSweeper.Core;
using com.eyerunnman.HexSweeper.Core.Commands;
using com.eyerunnman.patterns;
using UnityEngine;

namespace com.eyerunnman.HexSweeper.Core
{
    [RequireComponent(typeof(HexSweeperBehaviourProxy))]
    public class HexSweeperProxyController : GameService
    {
        HexSweeperBehaviourProxy proxy;

        [SerializeField]
        HexSweeperSettingsContainer settingsContainer;

        private void Awake()
        {
            proxy = GetComponent<HexSweeperBehaviourProxy>();
            ServiceLocator.Current.Register<HexSweeperProxyController>(this);
        }

        [ContextMenu("Setup Game")]
        public void SetupGame()
        {
            ICommand<HexSweeperBehaviour> resetGame = new SetupGame(settingsContainer);
            proxy.ExecuteCommand(resetGame);
        }
        [ContextMenu("Set to Idle")]
        public void SetGameToIdle()
        {
            ICommand<HexSweeperBehaviour> setToIdle = new SetHexGridToIdle();
            proxy.ExecuteCommand(setToIdle);
        }
        [ContextMenu("Pause Game")]
        public void PauseGame()
        {
            ICommand<HexSweeperBehaviour> pauseCommand = new PauseHexSweeper();
            proxy.ExecuteCommand(pauseCommand);
        }

        [ContextMenu("Resume Game")]
        public void ResumeGame()
        {
            ICommand<HexSweeperBehaviour> pauseCommand = new ResumeHexSweeper();
            proxy.ExecuteCommand(pauseCommand);
        }

        public void MoveHilightCellInDirection(Direction directions)
        {
            ICommand<HexSweeperBehaviour> moveHighlightCellDirection = new MoveHighlightCellInDirection(directions);
            proxy.ExecuteCommand(moveHighlightCellDirection);
        }

        [ContextMenu("Move UP")]
        public void MoveHighlightUp()
        {
            MoveHilightCellInDirection(Direction.North);
        }


        [ContextMenu("Move Down")]
        public void MoveHighlightDown()
        {
            MoveHilightCellInDirection(Direction.South);
        }

        [ContextMenu("Move Left")]
        public void MoveHighlightLeft()
        {
            MoveHilightCellInDirection(Direction.West);
        }

        [ContextMenu("Move Right")]
        public void MoveHighlightRight()
        {
            MoveHilightCellInDirection(Direction.East);
        }

        [ContextMenu("Reveal Cell")]
        public void RevealHighlightCell()
        {
            ICommand<HexSweeperBehaviour> revealHilightCell = new RevealCell();
            proxy.ExecuteCommand(revealHilightCell);
        }

        [ContextMenu("Flag Cell")]
        public void FlaglHighlightCell()
        {
            ICommand<HexSweeperBehaviour> revealHilightCell = new FlagCell();
            proxy.ExecuteCommand(revealHilightCell);
        }
    }

}
