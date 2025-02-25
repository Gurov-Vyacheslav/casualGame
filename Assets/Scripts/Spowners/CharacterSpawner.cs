using LearnGame.Enemy;
using UnityEngine;

namespace LearnGame.Spawners
{
    public class CharacterSpawner : BaseSpawner
    {
        [SerializeField]
        private PlayerCharacter _playerPrefab;
        [SerializeField]
        private EnemyCharacter _enemyPrefab;

        public CharacterSpawnersController CharacterSpawnersController {get; private set;}

        protected override void Awake()
        {
            base.Awake();
            CharacterSpawnersController = transform.parent.GetComponent<CharacterSpawnersController>();
        }

        protected void Start()
        {
            if (!CharacterSpawnersController.PlayerWasSpawned)
            {
                SpawningCharacter(_playerPrefab);
                CharacterSpawnersController.ReportSpawnPlayer();
            }
        }

        protected void Update()
        {
            if (CharacterSpawnersController.CurrentCountEnemy < CharacterSpawnersController.CountEnemy)
                UpdateSpawningEnemy();
        }

        private void UpdateSpawningEnemy()
        {
            _currentSpawnTimerSeconds += Time.deltaTime;
            if (_currentSpawnTimerSeconds > _currentSpawnIntervalSeconds)
            {
                _currentSpawnTimerSeconds = 0f;
                SetNewSpawnIntervalSeconds();

                SpawningCharacter(_enemyPrefab);
                CharacterSpawnersController.ReportSpawnEnemy();
            }
        }

        private void SpawningCharacter(BaseCharacter character)
        {
            var randomPointInsideRange = Random.insideUnitCircle * _range;
            var randomPosition = new Vector3(randomPointInsideRange.x, 0f, randomPointInsideRange.y) + transform.position;

            Instantiate(character, randomPosition, Quaternion.identity, transform);
        }
    }
}