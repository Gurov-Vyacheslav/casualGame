using UnityEngine;

namespace LearnGame.Spawners
{
    public class CharacterSpawnersController : MonoBehaviour
    {
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
            PlayerWon = CurrentCountKilledEnemy == CountEnemy;
        }

        public void ReportSpawnPlayer() => PlayerWasSpawned = true;

        public void ReportKillPlayer() => PlayerWasKilled = true;

        private int GetRandomCountEnemy() => Random.Range(_minCountEnemy, _maxCountEnemy);
    }
}