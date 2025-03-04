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
                var player = SpawningCharacter(_playerPrefab);
                CharacterSpawnersController.ReportSpawnPlayer(player);
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

                var enemy = SpawningCharacter(_enemyPrefab);
                CharacterSpawnersController.ReportSpawnEnemy((EnemyCharacter)enemy);
            }
        }

        private BaseCharacter SpawningCharacter(BaseCharacter character)
        {
            var randomPointInsideRange = Random.insideUnitCircle * _range;
            var randomPosition = new Vector3(randomPointInsideRange.x, 0f, randomPointInsideRange.y) + transform.position;

            return Instantiate(character, randomPosition, Quaternion.identity, transform);
        }
    }
}