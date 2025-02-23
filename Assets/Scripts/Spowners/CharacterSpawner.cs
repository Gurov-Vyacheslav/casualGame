using LearnGame.Enemy;
using UnityEngine;

namespace LearnGame.Spowners
{
    public class CharacterSpowner : BaseSpowner
    {
        [SerializeField]
        private PlayerCharacter _playerPrefab;
        [SerializeField]
        private EnemyCharacter _enemyPrefab;

        private CharacterSpawnersController _characterSpawnersController;

        protected override void Awake()
        {
            base.Awake();
            _characterSpawnersController = transform.parent.GetComponent<CharacterSpawnersController>();
        }

        protected void Start()
        {
            if (!_characterSpawnersController.PlayerWasSpawned)
            {
                SpawningCharacter(_playerPrefab);
                _characterSpawnersController.ReportSpownPlayer();
            }
        }

        protected void Update()
        {
            if (_characterSpawnersController.CurrentCountEnemy < _characterSpawnersController.CountEnemy)
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
                _characterSpawnersController.ReportSpownEnemy();
            }
        }

        private void SpawningCharacter(BaseCharacter character)
        {
            var randomPointInsideRange = Random.insideUnitCircle * _range;
            var randomPosition = new Vector3(randomPointInsideRange.x, 1f, randomPointInsideRange.y) + transform.position;

            Instantiate(character, randomPosition, Quaternion.identity, transform);
        }
    }
}