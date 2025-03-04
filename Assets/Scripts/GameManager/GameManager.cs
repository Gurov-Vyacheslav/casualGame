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

        private CounterEnemyUi _counterEnemy;
        private void Start()
        {
            _characterSpawnersController = FindObjectOfType<CharacterSpawnersController>();
            _timer = FindObjectOfType<TimerUI>();
            _pauseButton = FindObjectOfType<PauseUi>();
            _pauseButton = FindObjectOfType<PauseUi>();
            _counterEnemy = FindObjectOfType<CounterEnemyUi>();

            _characterSpawnersController.DeadPlayer += OnPlayerDead;

            _characterSpawnersController.WinPlayer += OnPlayerWin;

            _characterSpawnersController.KillEnemy += OnLKillEnemy;
            _counterEnemy.SetMaxCountEnemy(_characterSpawnersController.CountEnemy);

            _timer.TimeEnd += PlayerLose;

            _pauseButton.PressButton += StopGame;
        }

        private void OnPlayerDead()
        {
            OffPauseButton();

            _characterSpawnersController.DeadPlayer -= OnPlayerDead;
            Loss?.Invoke();
        }

        private void OnPlayerWin()
        {
            OffPauseButton();
            _characterSpawnersController.KillEnemy -= OnLKillEnemy;
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
            return Time.timeScale < 0.1f;
        }

        private void OffPauseButton()
        {
            if (_pauseButton != null) 
                _pauseButton.gameObject.SetActive(false);
        }

        private void OnLKillEnemy()
        {
            _counterEnemy.KilEnemy();
        }
    }
}