using LearnGame.Spawners;
using LearnGame.Ui;
using LearnGame.UI;
using System;
using UnityEngine;

namespace LearnGame
{
    public class GameManager : MonoBehaviour
    {
        public event Action Win;
        public event Action Loss;
        public event Action PressPause;

        private CharacterSpawnersController _characterSpawnersController;

        private TimerUI _timer;

        private PauseUi _pauseButton;
        private void Start()
        {
            _characterSpawnersController = FindObjectOfType<CharacterSpawnersController>();
            _timer = FindObjectOfType<TimerUI>();
            _pauseButton = FindObjectOfType<PauseUi>();

            _characterSpawnersController.DeadPlayer += OnPlayerDead;

            _characterSpawnersController.WinPlayer += OnPlayerWin;

            _timer.TimeEnd += PlayerLose;

            _pauseButton.PressButton += StopGame;
        }

        private void OnPlayerDead()
        {
            _characterSpawnersController.DeadPlayer -= OnPlayerDead;
            Loss?.Invoke();
        }

        private void OnPlayerWin()
        {
            _characterSpawnersController.WinPlayer -= OnPlayerWin;
            Win?.Invoke();
        }

        private void PlayerLose()
        {
            _timer.TimeEnd -= PlayerLose;
            Loss?.Invoke();
            Time.timeScale = 0;
        }

        private void StopGame()
        {
            PressPause?.Invoke();
            if (TimePoused())
                Time.timeScale = 1f;
            else
                Time.timeScale = 0f;
        }

        private bool TimePoused()
        {
            return Time.timeScale < 0.5f;
        }

    }
}