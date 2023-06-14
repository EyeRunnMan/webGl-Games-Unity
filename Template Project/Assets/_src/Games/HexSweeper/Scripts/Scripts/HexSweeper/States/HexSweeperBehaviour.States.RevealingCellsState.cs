using com.eyerunnman.MnSwpr;
using com.eyerunnman.patterns;
namespace com.eyerunnman.HexSweeper.Core.States
{
    internal class RevealingCellsState : RootAbstractTriggerableState<HexSweeperBehaviour, HexSweeperState, HexSweeperTrigger>
    {
        MineSweeperCellData cellToReveal;
        public RevealingCellsState(IAbstractStateMachine<HexSweeperBehaviour, HexSweeperState, HexSweeperTrigger> context, HexSweeperState stateID)
            : base(context, stateID) { }
    
        protected override HexSweeperState UndefinedState => HexSweeperState.Undefined;
    
        protected override void OnStateSetup()
        {
            AddTransition(HexSweeperState.WaitingForInput);
            AddTransition(HexSweeperState.GameLost);
            AddTransition(HexSweeperState.GameWon);
            AddTransition(HexSweeperState.Idle);
    
        }
    
        protected override void OnStateAwake()
        {
            
        }
    
        protected override void OnStateUpdate()
        {
            StateData.RevealCell stateData = Ctx.InterStateDataDictionary[StateID] as StateData.RevealCell;
            cellToReveal = Ctx.GetCellDataFromId(stateData.CellToReveal);
            Ctx.HexSweeper.RevealCell(cellToReveal);
            switch (Ctx.HexSweeper.Status)
            {
                case MineSweeperEnums.Status.RevealedAllMines:
                    InvokeTransition(HexSweeperState.GameLost);
                    break;
                case MineSweeperEnums.Status.RevealedAllNonMines:
                    InvokeTransition(HexSweeperState.GameWon);
                    break;
                case MineSweeperEnums.Status.Default:
                    InvokeTransition(HexSweeperState.WaitingForInput);
                    break;
            }
        }
    
        protected override void OnMultipleTriggers()
        {
            if (IsTriggerPresentCurrentFrame(HexSweeperTrigger.SetToIdle))
            {
                InvokeTransition(HexSweeperState.Idle);
            }
        }
    }
}
