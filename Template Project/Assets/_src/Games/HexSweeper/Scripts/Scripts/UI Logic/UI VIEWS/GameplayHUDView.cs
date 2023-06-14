using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace com.eyerunnman.HexSweeper.Ui.Views
{
    public class GameplayHUDView : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI NumberOfCellsFlaggedText;

        [SerializeField]
        Button PauseButton;

        [SerializeField]
        Toggle SoundToggle;

        internal Action OnPauseButtonClicked;
        internal Action<bool> OnSoundToggle;

        private void OnEnable()
        {
            PauseButton.onClick.AddListener(ExecutePauseOnClick);
            SoundToggle.onValueChanged.AddListener(ExecuteSoundToggle);
        }
        private void OnDisable()
        {
            PauseButton.onClick.RemoveListener(ExecutePauseOnClick);
            SoundToggle.onValueChanged.RemoveListener(ExecuteSoundToggle);
        }

        private void ExecutePauseOnClick()
        {
            OnPauseButtonClicked?.Invoke();
        }
        private void ExecuteSoundToggle(bool status)
        {
            OnSoundToggle?.Invoke(status);
        }

        internal void SetNumberOfCellsFlaggedText(string text) => NumberOfCellsFlaggedText.text = text;
    }
}

