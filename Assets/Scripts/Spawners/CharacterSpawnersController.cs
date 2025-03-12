using System;
using UnityEngine;
using LearnGame.Enemy;
using LearnGame.Timer;

namespace LearnGame.Spawners
{
    [DefaultExecutionOrder(-20)]
    public class CharacterSpawnersController : MonoBehaviour
    {
        public static CharacterSpawnersController instance { get; private set; }

        public event Action DeadPlayer;
        public event Action WinPlayer;
        public event Action KillEnemy;
        public event Action<EnemyCharacterView> SpawnEnemy;
        public event Action<BaseCharacterView> SpawnPlayer;

        [SerializeField]
        private int _minCountEnemy = 2;
        [SerializeField]
        private int _maxCountEnemy = 4;

        public int CountEnemy { get; private set; }
        public int CurrentCountEnemy { get; private set; } = 0;
        public bool PlayerWasSpawned { get; private set; } = false;

        public bool PlayerWasKilled { get; private set; } = false;
        public bool PlayerWon {  get; private set; } = false;
        public int CurrentCountKilledEnemy { get; private set; } = 0;

        public PlayerCharacterView Player {  get; private set; }

        public ITimer Timer { get; private set; }

        protected void Awake()
        {
            if (instance == null)
                instance = this;
            else
            {
                Destroy(this);
                return;
            }
            Timer = new UnityTimer();
            CountEnemy = GetRandomCountEnemy();
        }

        public void ReportSpawnEnemy(EnemyCharacterView enemy)
        {
            CurrentCountEnemy++;
            SpawnEnemy?.Invoke(enemy);
        }
        
        public void ReportKillEnemy()
        {
            CurrentCountKilledEnemy++;
            PlayerWon = CurrentCountKilledEnemy == CountEnemy && !PlayerWasKilled;

            KillEnemy?.Invoke();
            if (PlayerWon)
                WinPlayer?.Invoke();
        }

        public void ReportSpawnPlayer(PlayerCharacterView player)
        {
            PlayerWasSpawned = true;
            Player = player;
            SpawnPlayer?.Invoke(player);
        }

        public void ReportKillPlayer()
        {
            PlayerWasKilled = true;
            if (CurrentCountKilledEnemy != CountEnemy)
                DeadPlayer?.Invoke();
        }
        private int GetRandomCountEnemy() => UnityEngine.Random.Range(_minCountEnemy, _maxCountEnemy);
    }
}