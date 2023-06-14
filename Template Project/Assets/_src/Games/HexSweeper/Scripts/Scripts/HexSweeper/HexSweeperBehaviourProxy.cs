using com.eyerunnman.patterns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace com.eyerunnman.HexSweeper.Core{
    [RequireComponent(typeof(HexSweeperBehaviour))]
    public class HexSweeperBehaviourProxy : GameService, IProxy<HexSweeperBehaviour>
    {
        HexSweeperBehaviour context;

        public void ExecuteCommand(ICommand<HexSweeperBehaviour> command)
        {
            command.Execute(context);
        }

        private void Awake()
        {
            context = GetComponent<HexSweeperBehaviour>();
            ServiceLocator.Current.Register(this);
        }

        private void OnEnable()
        {
            context.OnWin += OnWinCallback;
            context.OnLose += OnLostCallBack;
            context.OnCellFlagged = OnFlaggedCallback;
        }

        private void OnDisable()
        {
            context.OnWin -= OnWinCallback;
            context.OnLose -= OnLostCallBack;
            context.OnCellFlagged -= OnFlaggedCallback;
        }

        private void OnWinCallback()
        {
            OnWin?.Invoke();
        }

        private void OnLostCallBack()
        {
            OnLose?.Invoke();
        }
        private void OnFlaggedCallback(int number)
        {
            OnCellFlagged?.Invoke(number);
        }

       

        public Action OnWin;
        public Action OnLose;
        public Action<int> OnCellFlagged;
        public int TotalMineCount => context.MineCount;
        public int TotalAvailabeFlags => TotalFlaggedCount - TotalMineCount;
        public int TotalFlaggedCount => context.FlaggedCount;
    }
}
