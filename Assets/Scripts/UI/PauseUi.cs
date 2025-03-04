using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace LearnGame.UI
{

    public class PauseUi : MonoBehaviour
    {
        public event Action PressButton;

        private Button StopGameButton;

        private void Start()
        {
            StopGameButton = GetComponent<Button>();
            StopGameButton.onClick.RemoveAllListeners();
            StopGameButton.onClick.AddListener(IncudePause);
        }

        private void IncudePause()
        {
            PressButton?.Invoke();
        }
    }
}
