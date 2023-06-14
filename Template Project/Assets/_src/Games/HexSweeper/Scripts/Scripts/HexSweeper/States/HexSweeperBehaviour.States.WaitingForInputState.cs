using com.eyerunnman.MnSwpr;
using com.eyerunnman.patterns;
namespace com.eyerunnman.HexSweeper.Core.States
{
    internal class WaitingForInputState : RootAbstractTriggerableState<HexSweeperBehaviour, HexSweeperState, HexSweeperTrigger>
    {
        public WaitingForInputState(IAbstractStateMachine<HexSweeperBehaviour, HexSweeperState, HexSweeperTrigger> context, HexSweeperState stateID)
            : base(context, stateID) { }
    
        protected override HexSweeperState UndefinedState => HexSweeperState.Undefined;
    
        protected override void OnStateSetup()
        {
            AddTransition(HexSweeperState.RevealingCells);
            AddTransition(HexSweeperState.Idle);

            LoadChildState(HexSweeperState.A_HighlightSelection);
    
        }
    
        protected override void OnMultipleTriggers()
        {
            if (IsTriggerPresentCurrentFrame(HexSweeperTrigger.SetToIdle))
            {
                InvokeTransition(HexSweeperState.Idle);
            }
            else if (IsTriggerPresentCurrentFrame(HexSweeperTrigger.ResetGame))
            {
                TriggerData.Reset resetTriggerData = Ctx.TriggerDataDict[HexSweeperTrigger.ResetGame] as TriggerData.Reset;
                HexSweeperSettingsContainer hexSweeperSettingsContainer = resetTriggerData.HexSweeperSettingsContainer;
                Ctx.InterStateDataDictionary[HexSweeperState.A_ResettingCells] = new StateData.Reset(hexSweeperSettingsContainer);
                InvokeTransition(HexSweeperState.Idle, HexSweeperState.A_ResettingCells);
            }
            else if (IsTriggerPresentCurrentFrame(HexSweeperTrigger.RevealCell))
            {
                Ctx.InterStateDataDictionary[HexSweeperState.RevealingCells] = new StateData.RevealCell(Ctx.HexSweeperSelectedCellHighlight.HilightedCellData.CellId);
                InvokeTransition(HexSweeperState.RevealingCells);
            }
            else if(IsTriggerPresentCurrentFrame(HexSweeperTrigger.FlagCell))
            {
                Ctx.FlagCell();
            }
            else if (IsTriggerPresentCurrentFrame(HexSweeperTrigger.HighlightCell))
            {
                MineSweeperCellData cellToReveal;
    
                if (Ctx.TriggerDataDict[HexSweeperTrigger.HighlightCell] is TriggerData.HighlightCell highlightCellData)
                {
                    cellToReveal = Ctx.GetCellDataFromId(highlightCellData.CellToReveal);
                }
                else
                {
                    cellToReveal = Ctx.HexSweeperCellRefs[0].MineSweeperCellData;
                }
    
                Ctx.InterStateDataDictionary[HexSweeperState.A_HighlightSelection] = new StateData.HighlightCell(cellToReveal.CellId);
    
                SwapChildState(HexSweeperState.A_HighlightSelection);
            }
        }

        
    }
}
