using com.eyerunnman.HexSweeper.Core;
using com.eyerunnman.patterns;
using com.eyerunnman.HexSweeper.Ui.States;
using com.eyerunnman.HexSweeper.Ui.Views;
using com.eyerunnman.HexSweeper.Audio;

namespace com.eyerunnman.HexSweeper.Ui
{
    public class HexSweeperUiBehaviour : MonobehaviourAbstractStateMachine<HexSweeperUiBehaviour, HexSweeperUIStates, HexSweeperUITriggers>
    {

        internal InitializeUIBehaviourData UiViewData { get; private set; }
        internal HexSweeperProxyController HexSweeperController { get; private set; }
        internal HexSweeperBehaviourProxy HexSweeperProxy { get; private set; }
        internal AudioManager AudioManager { get; private set; }
        internal bool IsWin;
        protected override HexSweeperUIStates RootEnum => HexSweeperUIStates.Base;

        // Start is called before the first frame update

        private void Awake()
        {
            
        }
        void Start()
        {
            SetupStateMachine();
            HexSweeperController = ServiceLocator.Current.Get<HexSweeperProxyController>();
            HexSweeperProxy = ServiceLocator.Current.Get<HexSweeperBehaviourProxy>();
            AudioManager = ServiceLocator.Current.Get<AudioManager>();
            void SetupStateMachine()
            {
                Ctx = this;
                StateFactory = new UiStateFactory(Ctx);
                ComputeStateTree();
                CurrentRootState = StateFactory.Create(HexSweeperUIStates.MainMenu);
                CurrentRootState?.ExecuteStateEnter();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
        internal void InitializeHexSweeperUiViews(InitializeUIBehaviourData data)
        {
            UiViewData = data;
        }



    }
    public enum HexSweeperUIStates
    {
        Base,
        Undefined,
        MainMenu,
        GameplayHUD,
        Pause,
        Result
    }
    public enum HexSweeperUITriggers
    {
        Undefined
    }

}

namespace com.eyerunnman.HexSweeper.Ui.Commands
{
}

