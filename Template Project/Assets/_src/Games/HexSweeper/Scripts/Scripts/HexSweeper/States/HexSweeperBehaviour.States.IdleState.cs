using com.eyerunnman.MnSwpr;
using com.eyerunnman.patterns;
using System.Collections.Generic;
using UnityEngine;

namespace com.eyerunnman.HexSweeper.Core.States
{
    internal class IdleState : RootAbstractTriggerableState<HexSweeperBehaviour, HexSweeperState, HexSweeperTrigger>
    {
        public IdleState(IAbstractStateMachine<HexSweeperBehaviour, HexSweeperState, HexSweeperTrigger> context, HexSweeperState stateID)
            : base(context, stateID) { }
    
        protected override HexSweeperState UndefinedState => HexSweeperState.Undefined;
    
        protected override void OnStateSetup()
        {
            AddTransition(HexSweeperState.WaitingForInput);
            LoadChildState(HexSweeperState.NULL_CHILD);
            LoadChildState(HexSweeperState.A_ResettingCells);
        }
    
        protected override void OnStateAwake()
        {
            Ctx.ResetHexSweeper();
        }
    
        protected override void OnMultipleTriggers()
        {
            if (IsTriggerPresentCurrentFrame(HexSweeperTrigger.StartGame))
            {
                InvokeTransition(HexSweeperState.WaitingForInput);
            }
            else
            if (IsTriggerPresentCurrentFrame(HexSweeperTrigger.ResetGame))
            {
                TriggerData.Reset resetTriggerData = Ctx.TriggerDataDict[HexSweeperTrigger.ResetGame] as TriggerData.Reset;
                HexSweeperSettingsContainer hexSweeperSettingsContainer = resetTriggerData.HexSweeperSettingsContainer;
                Ctx.InterStateDataDictionary[HexSweeperState.A_ResettingCells] = new StateData.Reset(hexSweeperSettingsContainer);
                SwapChildState(HexSweeperState.A_ResettingCells);
            }
        }
    }
}
