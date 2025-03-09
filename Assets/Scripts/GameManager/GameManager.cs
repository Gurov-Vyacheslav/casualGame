using LearnGame.Enemy;
using LearnGame.Spawners;
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
        public event Action<EnemyCharacter> SpawnEnemyPointer;
        public event Action<BaseCharacter> SpawnPlayer;

        [SerializeField]
        private CharacterSpawnersController _characterSpawnersController;

        private TimerUI _timer;

        private PauseUI _pauseButton;

        private CounterEnemyUI _counterEnemy;

        private void Awake()
        {
            Time.timeScale = 1;
            _characterSpawnersController.SpawnPlayer += OnSpawnPlayer;

            _timer = FindObjectOfType<TimerUI>();
            _pauseButton = FindObjectOfType<PauseUI>();
            _counterEnemy = FindObjectOfType<CounterEnemyUI>();
        }
        private void Start()
        {

            _characterSpawnersController.DeadPlayer += OnPlayerDead;

            _characterSpawnersController.WinPlayer += OnPlayerWin;

            _characterSpawnersController.KillEnemy += OnLKillEnemy;
            _counterEnemy.SetMaxCountEnemy(_characterSpawnersController.CountEnemy);

            _characterSpawnersController.SpawnEnemy += OnSpawnEnemy;

            _timer.TimeEnd += PlayerLose;

            _pauseButton.PressButton += StopGame;
        }

        private void OnPlayerDead()
        {
            OffPauseButton();
            BigUnsubscribe();
            Loss?.Invoke();
        }

        private void OnPlayerWin()
        {
            OffPauseButton();
            BigUnsubscribe();
            Win?.Invoke();
        }

        private void PlayerLose()
        {
            BigUnsubscribe();
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

        private void OnSpawnEnemy(EnemyCharacter enemy)
        {
            SpawnEnemyPointer?.Invoke(enemy);
        }

        private void OnSpawnPlayer(BaseCharacter player)
        {
            _characterSpawnersController.SpawnPlayer -= OnSpawnPlayer;
            
            SpawnPlayer?.Invoke(player);
        }


        private void BigUnsubscribe()
        {
            _characterSpawnersController.KillEnemy -= OnLKillEnemy;
            _characterSpawnersController.WinPlayer -= OnPlayerWin;
            _characterSpawnersController.SpawnEnemy -= OnSpawnEnemy;
        }
    }
}