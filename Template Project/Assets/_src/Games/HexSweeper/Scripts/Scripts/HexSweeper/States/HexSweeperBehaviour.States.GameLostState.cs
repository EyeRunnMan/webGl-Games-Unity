using com.eyerunnman.patterns;
namespace com.eyerunnman.HexSweeper.Core.States
{
    internal class GameLostState : RootAbstractTriggerableState<HexSweeperBehaviour, HexSweeperState, HexSweeperTrigger>
    {
        public GameLostState(IAbstractStateMachine<HexSweeperBehaviour, HexSweeperState, HexSweeperTrigger> context, HexSweeperState stateID)
            : base(context, stateID) { }
        
        protected override HexSweeperState UndefinedState => HexSweeperState.Undefined;
    
        protected override void OnStateSetup()
        {
            AddTransition(HexSweeperState.Idle);
    
        }
        protected override void OnStateAwake()
        {
            Ctx.OnLose?.Invoke();
        }

        protected override void OnMultipleTriggers()
        {
            if (IsTriggerPresentCurrentFrame(HexSweeperTrigger.SetToIdle))
            {
                InvokeTransition(HexSweeperState.Idle);
            }
            else if (IsTriggerPresentCurrentFrame(HexSweeperTrigger.ResetGame))
            {
                HexSweeperSettingsContainer hexSweeperSettingsContainer = (Ctx.TriggerDataDict[HexSweeperTrigger.ResetGame] as TriggerData.Reset).HexSweeperSettingsContainer;
                Ctx.InterStateDataDictionary[HexSweeperState.A_ResettingCells] = new StateData.Reset(hexSweeperSettingsContainer);
                InvokeTransition(HexSweeperState.Idle, HexSweeperState.A_ResettingCells);
            }
        }
    }
}
