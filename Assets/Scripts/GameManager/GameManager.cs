using LearnGame.Enemy;
using LearnGame.Spawners;
using LearnGame.Timer;
using LearnGame.UI;
using System;
using UnityEngine;

namespace LearnGame
{
    [DefaultExecutionOrder(-10)]
    public class GameManager : MonoBehaviour, IBaseUI
    {
        public static GameManager Instance {  get; private set; }
        public ITimer Timer { get; private set; }

        public event Action Win;
        public event Action Loss;
        public event Action PressPause;
        public event Action<EnemyCharacterView> SpawnEnemyPointer;
        public event Action<BaseCharacterView> SpawnPlayer;

        private TimerUIView _timerUI;

        private PauseUI _pauseButton;

        private CounterEnemyUI _counterEnemy;

        private AudioSource _cameraSound;

        public void SetTimerUI(TimerUIView timer)
        {
            _timerUI = timer;
            _timerUI.TimeEnd += PlayerLose;
        }
        public void SetPauseButtonUI(PauseUI pauseButton)
        {
            _pauseButton = pauseButton;
            _pauseButton.PressButton += StopGame;
        }
        public void SetCounterEnemy(CounterEnemyUI counterEnemy)
        {
            _counterEnemy = counterEnemy;
            _counterEnemy.SetMaxCountEnemy(CharacterSpawnersController.Instance.CountEnemy);
        }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
            {
                Destroy(this);
                return;
            }
            Timer = CharacterSpawnersController.Instance.Timer;
            Time.timeScale = 1;

            _cameraSound = UnityEngine.Camera.main.GetComponent<AudioSource>();
            CharacterSpawnersController.Instance.SpawnPlayer += OnSpawnPlayer;

            CharacterSpawnersController.Instance.DeadPlayer += OnPlayerDead;

            CharacterSpawnersController.Instance.WinPlayer += OnPlayerWin;

            CharacterSpawnersController.Instance.KillEnemy += OnLKillEnemy;

            CharacterSpawnersController.Instance.SpawnEnemy += OnSpawnEnemy;
        }

        protected void OnDestroy()
        {
            if (CharacterSpawnersController.Instance != null)
            {
                CharacterSpawnersController.Instance.SpawnPlayer -= OnSpawnPlayer;

                CharacterSpawnersController.Instance.DeadPlayer -= OnPlayerDead;

                CharacterSpawnersController.Instance.WinPlayer -= OnPlayerWin;

                CharacterSpawnersController.Instance.KillEnemy -= OnLKillEnemy;

                CharacterSpawnersController.Instance.SpawnEnemy -= OnSpawnEnemy;
            }
            if (_timerUI != null)
                _timerUI.TimeEnd -= PlayerLose;
            if (_pauseButton != null)
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
            CharacterSpawnersController.Instance.SpawnPlayer -= OnSpawnPlayer;
            
            SpawnPlayer?.Invoke(player);
        }


        private void BigUnsubscribe()
        {
            CharacterSpawnersController.Instance.KillEnemy -= OnLKillEnemy;
            CharacterSpawnersController.Instance.WinPlayer -= OnPlayerWin;
            CharacterSpawnersController.Instance.SpawnEnemy -= OnSpawnEnemy;
        }
    }
}