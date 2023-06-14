using com.eyerunnman.MnSwpr;
using System.Collections.Generic;
using UnityEngine;
using com.eyerunnman.patterns;
using com.eyerunnman.Helper;
using com.eyerunnman.HexSweeper.Core.States;
using System;
using System.Linq;

namespace com.eyerunnman.HexSweeper.Core
{
    [RequireComponent(typeof(Grid))]
    public partial class HexSweeperBehaviour : MonobehaviourAbstractStateMachine<HexSweeperBehaviour,HexSweeperState,HexSweeperTrigger>
    {
        private MineSweeper hexSweeper;
        HexSweeperSelectedCellHighlight hexSweeperSelectedCellHighlight;
        HashSet<HexSweeperCellBehaviour> hexSweeperCellRefs;
        HashSet<int> flaggedCells;

        private Dictionary<HexSweeperTrigger,object> triggerDataDict;
        private Dictionary<HexSweeperState, object> interStateDataDictionary;

        internal Action OnWin;
        internal Action OnLose;
        internal Action<int> OnCellFlagged;


        #region Internal Properties

        internal MineSweeper HexSweeper => hexSweeper;
        internal List<HexSweeperCellBehaviour> HexSweeperCellRefs => new(hexSweeperCellRefs);
        internal HexSweeperSelectedCellHighlight HexSweeperSelectedCellHighlight => hexSweeperSelectedCellHighlight;

        internal Dictionary<HexSweeperTrigger, object> TriggerDataDict => triggerDataDict;
        internal Dictionary<HexSweeperState, object> InterStateDataDictionary => interStateDataDictionary;
        internal HashSet<int> FlaggedCells => flaggedCells;
        internal int HighlightedCellId => hexSweeperSelectedCellHighlight != null ? hexSweeperSelectedCellHighlight.HilightedCellData.CellId : -1;
        internal int MineCount => hexSweeper.MineCount;
        internal int AvailableFlagCount => MineCount - FlaggedCount;
        internal int FlaggedCount => FlaggedCells.Count;

        #endregion
        protected override HexSweeperState RootEnum => HexSweeperState.Base;

        #region Monobehaviour Life Cycle

        private void Awake()
        {
            SetupStateMachine();
            InitializeLocalVariables();

            void SetupStateMachine()
            {
                Ctx = this;
                StateFactory = new StateEnumFactory(Ctx);
                ComputeStateTree();
                CurrentRootState = StateFactory.Create(HexSweeperState.Idle);
                CurrentRootState?.ExecuteStateEnter();
            }

            void InitializeLocalVariables()
            {
                hexSweeperCellRefs = new();
                CurrentFrameTriggers = new();
                triggerDataDict = new();

                interStateDataDictionary = new();
            }
        }
        private void Update()
        {
            ProcessCurrentFrameTriggers();
            CurrentRootState?.ExecuteStateUpdate();

            void ProcessCurrentFrameTriggers()
            {
                CurrentRootState?.ExecuteMultipleTriggers();
                CurrentFrameTriggers.Clear();
                TriggerDataDict.Clear();
            }

            string tmp = "";
            foreach (var state in CurrentStateChain)
            {
                tmp += "->" + state;
            }
            Debug.Log(tmp);
        }
        private void FixedUpdate()
        {
            CurrentRootState?.ExecuteStateFixedUpdate();
        }

        #endregion

        #region Command Functions
        internal void ResetHexSweeper()
        {
            transform.position = new Vector3();
            hexSweeperCellRefs ??= new();
            flaggedCells = new();
            if (Ctx.hexSweeperSelectedCellHighlight != null)
            {
                Destroy(Ctx.hexSweeperSelectedCellHighlight.gameObject);
            }
            foreach (var cell in Ctx.hexSweeperCellRefs)
            {
                Destroy(cell.gameObject);
            }
            Ctx.hexSweeperCellRefs.Clear();
        }
        internal void ResetGame(HexSweeperSettingsContainer hexSweeperSettingsContainer)
        {
            PushCurrentFrameTrigger(HexSweeperTrigger.ResetGame);
            TriggerDataDict[HexSweeperTrigger.ResetGame] = new TriggerData.Reset(hexSweeperSettingsContainer);
        }
        internal void RevealCell()
        {
            PushCurrentFrameTrigger(HexSweeperTrigger.RevealCell);
        }
        internal void HighlightCellTrigger(int cellIdToHighlight)
        {
            if(cellIdToHighlight != hexSweeperSelectedCellHighlight.HilightedCellData.CellId 
                && 
                cellIdToHighlight != MineSweeperCellData.Default.CellId)
            {
                PushCurrentFrameTrigger(HexSweeperTrigger.HighlightCell);
                TriggerDataDict[HexSweeperTrigger.HighlightCell] = new TriggerData.HighlightCell(cellIdToHighlight);
            }
        }
        internal void MoveHighlightCell(Direction direction)
        {
            PushCurrentFrameTrigger(HexSweeperTrigger.MoveHighlight);
            TriggerDataDict[HexSweeperTrigger.MoveHighlight] = new TriggerData.MoveHighlightCell(direction);
        }
        internal void SetHexGridToIdle()
        {
            PushCurrentFrameTrigger(HexSweeperTrigger.SetToIdle);
        }
        internal void PauseStateMachine()
        {
            CurrentRootState?.ExecuteStatePause();
        }
        internal void ResumeStateMachine()
        {
            CurrentRootState?.ExecuteStateResume();
        }
        internal void StartGame()
        {
            PushCurrentFrameTrigger(HexSweeperTrigger.StartGame);
        }
        internal void FlagCellTrigger   ()
        {
            PushCurrentFrameTrigger(HexSweeperTrigger.FlagCell);
        }
        #endregion

        #region Internal Methods
        internal MineSweeperCellData GetCellDataFromId(int cellId)
        {
            if(cellId == -1)
            {
                return MineSweeperCellData.Default;
            }

            return HexSweeper.GetCellData(cellId);
        }

        internal void SetupCellHighlight(HexSweeperSelectedCellHighlight prefab)
        {
            hexSweeperSelectedCellHighlight = Instantiate(prefab, Vector3.zero, Quaternion.identity, Ctx.transform);
            Ctx.HexSweeperSelectedCellHighlight.gameObject.SetActive(false);
        }
        internal void SetupCellRefs(HexSweeperCellBehaviour prefab,int totalCellCount)
        {
            for (int i = 0; i < totalCellCount; i++)
            {
                HexSweeperCellBehaviour cellTile = Instantiate(prefab, Vector3.zero, Quaternion.identity, Ctx.transform);
                hexSweeperCellRefs.Add(cellTile);
            }
        }
        internal void SetupHexSweeper(Grid grid, MineSweeperSettings settings, HashSet<IMineSweeperCell> hexSweeperCellSet)
        {
            hexSweeper = new MineSweeper( grid, settings, hexSweeperCellSet);
        }

        internal void HighlightCellInDirection(Direction direction)
        {
            if (direction == Direction.East)
            {
                direction = Direction.West;
            }
            else if (direction == Direction.West)
            {
                direction = Direction.East;
            }
            MineSweeperCellData otherCell = GetCellDataFromId(Ctx.HexSweeper.GetCellIdInDirection(HighlightedCellId, direction));
            if (otherCell.IsDefault is false)
            {
                HighlightCell(otherCell.CellId);
            }
        }
        internal void HighlightCell(int hilightedCellId)
        {
            MineSweeperCellData cellData = Ctx.GetCellDataFromId(hilightedCellId);
            HexSweeperSelectedCellHighlight.SetHighlightPosition(cellData);
        }
        internal void FlagCell()
        {
            MineSweeperCellData cellToFlag = Ctx.HexSweeperSelectedCellHighlight.HilightedCellData;
            HexSweeperCellBehaviour cellToFlagObject = hexSweeperCellRefs.First(data => data.MineSweeperCellData.CellId == HighlightedCellId);

            if (cellToFlag.IsDefault is false && cellToFlagObject != null )
            {
                
                if (Ctx.FlaggedCells.Contains(cellToFlag.CellId))
                {
                    Ctx.FlaggedCells.Remove(cellToFlag.CellId);
                    cellToFlagObject.UnFlagCell();
                }
                else if(AvailableFlagCount > 0)
                {
                    Ctx.FlaggedCells.Add(cellToFlag.CellId);
                    cellToFlagObject.FlagCell();
                }

                OnCellFlagged?.Invoke(FlaggedCells.Count);
            }
        }


        #endregion
        private void PushCurrentFrameTrigger(HexSweeperTrigger trigger)
        {
            CurrentFrameTriggers ??=new ();
            CurrentFrameTriggers.Add(trigger);
        }
        
    }
    public enum HexSweeperState
    {
        Undefined,
        Base,
        Idle,
        GameLost,
        GameWon,
        WaitingForInput,
        RevealingCells,
        A_ResettingCells,
        A_HighlightSelection,
        NULL_CHILD
    }
    public enum HexSweeperTrigger
    {
        SetToIdle,
        ResetGame,
        RevealCell,
        FlagCell,
        StartGame,
        HighlightCell,
        MoveHighlight,
    }
}
