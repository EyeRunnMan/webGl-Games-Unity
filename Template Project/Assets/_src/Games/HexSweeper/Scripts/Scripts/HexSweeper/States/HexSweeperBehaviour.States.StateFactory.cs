using com.eyerunnman.HexSweeper.Core.States;
using com.eyerunnman.patterns;
namespace com.eyerunnman.HexSweeper.Core.States
{
    internal class StateEnumFactory : IFactory<AbstractTriggerableState<HexSweeperBehaviour, HexSweeperState, HexSweeperTrigger>, HexSweeperState>
    {
        readonly HexSweeperBehaviour hexSweeperBehaviour; 
        public StateEnumFactory(HexSweeperBehaviour hexSweeperBehaviour)
        {
            this.hexSweeperBehaviour = hexSweeperBehaviour;
        }
        public AbstractTriggerableState<HexSweeperBehaviour, HexSweeperState, HexSweeperTrigger> Create(HexSweeperState type)
        {
            return type switch
            {
                HexSweeperState.GameLost => new GameLostState(hexSweeperBehaviour, type),
                HexSweeperState.GameWon => new GameWonState(hexSweeperBehaviour, type),
                HexSweeperState.WaitingForInput => new WaitingForInputState(hexSweeperBehaviour, type),
                HexSweeperState.RevealingCells => new RevealingCellsState(hexSweeperBehaviour, type),
                HexSweeperState.A_ResettingCells => new A_ResettingCellsState(hexSweeperBehaviour, type),
                HexSweeperState.Idle => new IdleState(hexSweeperBehaviour, type),
                HexSweeperState.A_HighlightSelection => new A_HighlightSelection(hexSweeperBehaviour, type),
                HexSweeperState.NULL_CHILD =>new NULL_CHILD_STATE(hexSweeperBehaviour, type),
                _ => null,
            };
        }
    }
}
