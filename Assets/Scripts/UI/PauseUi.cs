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

        private Button _stopGameButton;

        private void Start()
        {
            _stopGameButton = GetComponent<Button>();
            _stopGameButton.onClick.RemoveAllListeners();
            _stopGameButton.onClick.AddListener(IncudePause);
        }

        private void IncudePause()
        {
            PressButton?.Invoke();
        }
    }
}
