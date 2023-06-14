using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.eyerunnman.HexSweeper.Ui.Views
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField]
        Button PlayButton;

        internal Action OnPlayButtonClicked;
        private void OnEnable()
        {
            PlayButton.onClick.AddListener(ExecuteOnClick);
        }
        private void OnDisable()
        {
            PlayButton.onClick.RemoveListener(ExecuteOnClick);
        }

        private void ExecuteOnClick()
        {
            OnPlayButtonClicked?.Invoke();
        }
    }
}

