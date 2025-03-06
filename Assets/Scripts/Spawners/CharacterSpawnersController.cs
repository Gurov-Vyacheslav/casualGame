using System;
using UnityEngine;
using LearnGame.Enemy;

namespace LearnGame.Spawners
{
    public class CharacterSpawnersController : MonoBehaviour
    {
        public event Action DeadPlayer;
        public event Action WinPlayer;
        public event Action KillEnemy;
        public event Action<EnemyCharacter> SpawnEnemy;
        public event Action<BaseCharacter> SpawnPlayer;

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

        protected void Awake()
        {
            CountEnemy = GetRandomCountEnemy();
        }

        public void ReportSpawnEnemy(EnemyCharacter enemy)
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

        public void ReportSpawnPlayer(BaseCharacter player)
        {
            PlayerWasSpawned = true;
            SpawnPlayer?.Invoke(player);
        }

        public void ReportKillPlayer()
        {
            PlayerWasKilled = true;
            
            DeadPlayer?.Invoke();
        }
        private int GetRandomCountEnemy() => UnityEngine.Random.Range(_minCountEnemy, _maxCountEnemy);
    }
}