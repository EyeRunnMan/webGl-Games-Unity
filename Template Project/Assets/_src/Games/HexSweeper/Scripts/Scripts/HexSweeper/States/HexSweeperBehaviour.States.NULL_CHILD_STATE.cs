using com.eyerunnman.patterns;

namespace com.eyerunnman.HexSweeper.Core.States
{
    internal class NULL_CHILD_STATE : AbstractTriggerableState<HexSweeperBehaviour, HexSweeperState, HexSweeperTrigger>
    {
        public NULL_CHILD_STATE(IAbstractStateMachine<HexSweeperBehaviour, HexSweeperState, HexSweeperTrigger> context, HexSweeperState stateID)
           : base(context, stateID) { }
    
        protected override HexSweeperState UndefinedState => HexSweeperState.Undefined;
    }
}
