using LearnGame.Spawners;
using LearnGame.Ui;
using System;
using UnityEngine;

namespace LearnGame
{
    public class GameManager : MonoBehaviour
    {
        public event Action Win;
        public event Action Loss;

        private CharacterSpawnersController _characterSpawnersController;

        private TimerUI _timer;
        private void Start()
        {
            _characterSpawnersController = FindObjectOfType<CharacterSpawnersController>();
            _timer = FindObjectOfType<TimerUI>();

            _characterSpawnersController.DeadPlayer += OnPlayerDead;

            _characterSpawnersController.WinPlayer += OnPlayerWin;

            _timer.TimeEnd += PlayerLose;
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

    }
}