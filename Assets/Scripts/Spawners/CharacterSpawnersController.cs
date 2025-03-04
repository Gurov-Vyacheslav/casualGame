using System;
using UnityEngine;

namespace LearnGame.Spawners
{
    public class CharacterSpawnersController : MonoBehaviour
    {
        public event Action DeadPlayer;
        public event Action WinPlayer;

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

        public void ReportSpawnEnemy() => CurrentCountEnemy++;
        
        public void ReportKillEnemy()
        {
            CurrentCountKilledEnemy++;
            PlayerWon = CurrentCountKilledEnemy == CountEnemy && !PlayerWasKilled;

            if (PlayerWon)
                WinPlayer?.Invoke();
        }

        public void ReportSpawnPlayer() => PlayerWasSpawned = true;

        public void ReportKillPlayer()
        {
            PlayerWasKilled = true;
            if (CurrentCountKilledEnemy != CountEnemy)
                DeadPlayer?.Invoke();
        }
        private int GetRandomCountEnemy() => UnityEngine.Random.Range(_minCountEnemy, _maxCountEnemy);
    }
}