using LearnGame.CompositionRoot;
using LearnGame.Enemy;
using UnityEngine;

namespace LearnGame.Spawners
{
    public class CharacterSpawner : BaseSpawner
    {
        [SerializeField]
        private CharacterCompositionRoot _playerPrefab;
        [SerializeField]
        private CharacterCompositionRoot _enemyPrefab;


        protected override void Awake()
        {
            base.Awake();
        }

        protected void Start()
        {
            if (!CharacterSpawnersController.instance.PlayerWasSpawned)
            {
                var player = SpawningCharacter(_playerPrefab);
                CharacterSpawnersController.instance.ReportSpawnPlayer((PlayerCharacterView)player);
            }
        }

        protected void Update()
        {
            if (CharacterSpawnersController.instance.CurrentCountEnemy < CharacterSpawnersController.instance.CountEnemy)
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
                CharacterSpawnersController.instance.ReportSpawnEnemy((EnemyCharacterView)enemy);
            }
        }

        private BaseCharacterView SpawningCharacter(CharacterCompositionRoot characterCompositionRoot)
        {
            var randomPointInsideRange = Random.insideUnitCircle * _range;
            var randomPosition = new Vector3(randomPointInsideRange.x, 0f, randomPointInsideRange.y) + transform.position;
            var newCharacterCompositionRoot = Instantiate(characterCompositionRoot, randomPosition, Quaternion.identity, transform);

            return newCharacterCompositionRoot.Compose(CharacterSpawnersController.instance.Timer);
        }
    }
}