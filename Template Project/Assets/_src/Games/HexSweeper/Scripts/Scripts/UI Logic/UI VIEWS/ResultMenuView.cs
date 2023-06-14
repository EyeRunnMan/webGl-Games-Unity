using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace com.eyerunnman.HexSweeper.Ui.Views
{
    public class ResultMenuView : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI ResultText;

        [SerializeField]
        Button ReplayButton;

        [SerializeField]
        Button MainMenuButton;


        internal Action OnReplayButtonClicked;
        internal Action OnMainMenuButtonClicked;


        private void OnEnable()
        {
            ReplayButton.onClick.AddListener(ExecuteReplayOnClick);
            MainMenuButton.onClick.AddListener(ExecuteMainMenuOnClick);
        }
        private void OnDisable()
        {
            ReplayButton.onClick.RemoveListener(ExecuteReplayOnClick);
            MainMenuButton.onClick.RemoveListener(ExecuteMainMenuOnClick);
        }

        private void ExecuteReplayOnClick()
        {
            OnReplayButtonClicked?.Invoke();
        }
        private void ExecuteMainMenuOnClick()
        {
            OnMainMenuButtonClicked?.Invoke();
        }

        internal void SetResultText(string text) => ResultText.text = text;
    }
}
