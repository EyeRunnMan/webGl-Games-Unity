using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.eyerunnman.HexSweeper.Ui.Views
{
    public class PauseMenuView : MonoBehaviour
    {
        [SerializeField]
        Button resumeButton;

        [SerializeField]
        Button mainMenuButton;

        internal Action OnResumeButtonClicked;
        internal Action OnMainMenuButtonClicked;

        private void OnEnable()
        {
            resumeButton.onClick.AddListener(ExecuteResumeOnClick);
            mainMenuButton.onClick.AddListener(ExecuteMainMenuOnClick);
        }
        private void OnDisable()
        {
            resumeButton.onClick.RemoveListener(ExecuteResumeOnClick);
            mainMenuButton.onClick.RemoveListener(ExecuteMainMenuOnClick);
        }

        private void ExecuteResumeOnClick()
        {
            OnResumeButtonClicked?.Invoke();
        }
        private void ExecuteMainMenuOnClick()
        {
            OnMainMenuButtonClicked?.Invoke();
        }
    }
}

