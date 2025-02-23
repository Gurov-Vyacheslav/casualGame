using UnityEngine;
namespace LearnGame.Spowners
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

        protected void Awake()
        {
            CountEnemy = GetRandomCountEnemy();
        }

        public void ReportSpownEnemy() => CurrentCountEnemy++;
        public void ReportSpownPlayer() => PlayerWasSpawned = true;

        private int GetRandomCountEnemy() => Random.Range(_minCountEnemy, _maxCountEnemy);
    }
}