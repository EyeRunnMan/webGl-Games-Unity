using com.eyerunnman.Helper;
using com.eyerunnman.MnSwpr;
using com.eyerunnman.patterns;
using UnityEngine;

namespace com.eyerunnman.HexSweeper.Core.States
{
    internal class A_HighlightSelection : AbstractTriggerableState<HexSweeperBehaviour, HexSweeperState, HexSweeperTrigger>
    {
        public A_HighlightSelection(IAbstractStateMachine<HexSweeperBehaviour, HexSweeperState, HexSweeperTrigger> context, HexSweeperState stateID)
            : base(context, stateID) { }

        protected override HexSweeperState UndefinedState => HexSweeperState.Undefined;

        protected override void OnStateAwake()
        {
            Ctx.HighlightCell(Ctx.HexSweeperCellRefs[0].MineSweeperCellData.CellId);
            if (Ctx.InterStateDataDictionary.TryGetValue(HexSweeperState.A_HighlightSelection, out object data))
            {
                if (data as StateData.HighlightCell is not null)
                {
                    Ctx.HighlightCell(((data as StateData.HighlightCell).cellToHighlight));
                }
            }
            Ctx.HexSweeperSelectedCellHighlight.ToggleHighlight(true);
        }


        protected override void OnStateUpdate()
        {

        }

        protected override void OnStateExit()
        {
            Ctx.HexSweeperSelectedCellHighlight.ToggleHighlight(false);
        }

        protected override void OnMultipleTriggers()
        {
            if (IsTriggerPresentCurrentFrame(HexSweeperTrigger.MoveHighlight))
            {
                TriggerData.MoveHighlightCell moveHighlightTriggerData = Ctx.TriggerDataDict[HexSweeperTrigger.MoveHighlight] as TriggerData.MoveHighlightCell;

                Ctx.HighlightCellInDirection(moveHighlightTriggerData.moveToDirection);
            }
        }
    }
}
