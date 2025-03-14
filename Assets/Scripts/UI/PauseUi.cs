using UnityEngine;
using UnityEngine.UI;
using System;

namespace LearnGame.UI
{
    public class PauseUI : MonoBehaviour, IInitializerGameManeger
    {
        public event Action PressButton;

        private Button StopGameButton;

        public void InitializeGameManager()
        {
            GameManager.Instance.SetPauseButtonUI(this);
        }
        private void Start()
        {
            InitializeGameManager();
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
