using com.eyerunnman.MnSwpr;
using com.eyerunnman.patterns;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace com.eyerunnman.HexSweeper.Core.States
{
    internal class A_ResettingCellsState : AbstractTriggerableState<HexSweeperBehaviour, HexSweeperState, HexSweeperTrigger>
    {

        public A_ResettingCellsState(IAbstractStateMachine<HexSweeperBehaviour, HexSweeperState, HexSweeperTrigger> context, HexSweeperState stateID)
            : base(context, stateID) { }

        protected override HexSweeperState UndefinedState => HexSweeperState.Undefined;

        protected override void OnStateAwake()
        {
            SetupHexSweeper();

            void SetupHexSweeper()
            {
                StateData.Reset stateData = Ctx.InterStateDataDictionary[HexSweeperState.A_ResettingCells] as StateData.Reset;

                MineSweeperSettings mineSweeperSettings = stateData.HexSweeperSettingsContainer.MineSweeperSettings;
                HexSweeperCellBehaviour hexSweeperCellObject = stateData.HexSweeperSettingsContainer.CellPrefab;
                Grid gridComponent = Ctx.gameObject.GetComponent<Grid>();

                HexSweeperSelectedCellHighlight hexSweeperSelectCellObject = stateData.HexSweeperSettingsContainer.CellHighlightSelector;

                Ctx.SetupCellHighlight(hexSweeperSelectCellObject);
                Ctx.SetupCellRefs(hexSweeperCellObject, mineSweeperSettings.TotalCellCount);
                Ctx.SetupHexSweeper(gridComponent, mineSweeperSettings, Ctx.HexSweeperCellRefs.Select(data=> data as IMineSweeperCell).ToHashSet());

                Ctx.InterStateDataDictionary[HexSweeperState.A_HighlightSelection] = new StateData.HighlightCell(Ctx.HexSweeperCellRefs[0].MineSweeperCellData.CellId);

            Vector3 calculateCenter = new(); ;
                foreach (var item in Ctx.HexSweeperCellRefs)
                {
                    calculateCenter += item.transform.position;
                }
                Ctx.transform.position = new();
                Ctx.transform.position -= calculateCenter / Ctx.HexSweeperCellRefs.Count;
            }
        }

        protected override void OnStateUpdate()
        {
            Ctx.StartGame();
        }

    }
}
