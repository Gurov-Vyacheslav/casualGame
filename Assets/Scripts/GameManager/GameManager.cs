using LearnGame.Enemy;
using LearnGame.Spawners;
using LearnGame.Timer;
using LearnGame.UI;
using System;
using UnityEngine;

namespace LearnGame
{
    [DefaultExecutionOrder(-10)]
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance {  get; private set; }

        public event Action Win;
        public event Action Loss;
        public event Action PressPause;
        public event Action<EnemyCharacterView> SpawnEnemyPointer;
        public event Action<BaseCharacterView> SpawnPlayer;

        private TimerUI _timerUI;

        private PauseUI _pauseButton;

        private CounterEnemyUI _counterEnemy;

        private AudioSource _cameraSound;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
            {
                Destroy(this);
                return;
            }

            Time.timeScale = 1;

            _cameraSound = UnityEngine.Camera.main.GetComponent<AudioSource>();
            CharacterSpawnersController.instance.SpawnPlayer += OnSpawnPlayer;

            _timerUI = FindObjectOfType<TimerUI>();
            _pauseButton = FindObjectOfType<PauseUI>();
            _counterEnemy = FindObjectOfType<CounterEnemyUI>();

            CharacterSpawnersController.instance.DeadPlayer += OnPlayerDead;

            CharacterSpawnersController.instance.WinPlayer += OnPlayerWin;

            CharacterSpawnersController.instance.KillEnemy += OnLKillEnemy;
            _counterEnemy.SetMaxCountEnemy(CharacterSpawnersController.instance.CountEnemy);

            CharacterSpawnersController.instance.SpawnEnemy += OnSpawnEnemy;

            _timerUI.TimeEnd += PlayerLose;

            _pauseButton.PressButton += StopGame;
        }

        protected void OnDestroy()
        {
            CharacterSpawnersController.instance.SpawnPlayer -= OnSpawnPlayer;

            CharacterSpawnersController.instance.DeadPlayer -= OnPlayerDead;

            CharacterSpawnersController.instance.WinPlayer -= OnPlayerWin;

            CharacterSpawnersController.instance.KillEnemy -= OnLKillEnemy;

            CharacterSpawnersController.instance.SpawnEnemy -= OnSpawnEnemy;

            _timerUI.TimeEnd -= PlayerLose;

            _pauseButton.PressButton -= StopGame;
        }
        private void OnPlayerDead()
        {
            _cameraSound.enabled = false;
            OffPauseButton();
            BigUnsubscribe();
            Loss?.Invoke();
        }

        private void OnPlayerWin()
        {
            _cameraSound.enabled = false;
            OffPauseButton();
            BigUnsubscribe();
            Win?.Invoke();
        }

        private void PlayerLose()
        {
            _cameraSound.enabled = false;
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

        private void OnSpawnEnemy(EnemyCharacterView enemy)
        {
            SpawnEnemyPointer?.Invoke(enemy);
        }

        private void OnSpawnPlayer(BaseCharacterView player)
        {
            CharacterSpawnersController.instance.SpawnPlayer -= OnSpawnPlayer;
            
            SpawnPlayer?.Invoke(player);
        }


        private void BigUnsubscribe()
        {
            CharacterSpawnersController.instance.KillEnemy -= OnLKillEnemy;
            CharacterSpawnersController.instance.WinPlayer -= OnPlayerWin;
            CharacterSpawnersController.instance.SpawnEnemy -= OnSpawnEnemy;
        }
    }
}