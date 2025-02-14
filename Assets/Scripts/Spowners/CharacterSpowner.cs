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
        [SerializeField]
        private float _playerSpawnProbabilityPercent = 20f;

        private CharacterSpawnersController _characterSpawnersController;

        protected override void Awake()
        {
            base.Awake();
            _characterSpawnersController = transform.parent.GetComponent<CharacterSpawnersController>();
        }
        protected void Update()
        {

            if (_characterSpawnersController.PlayerWasSpowned)
            {
                if (_characterSpawnersController.CurrentCountEnemy < _characterSpawnersController.MinCountEnemy)
                    Spowning(_enemyPrefab);
            }
            else
            {
                if (_characterSpawnersController.CurrentCountEnemy < _characterSpawnersController.MaxCountEnemy)
                    Spowning(GetRandomCharacter());
                else
                    Spowning(_playerPrefab);
            }
        }
        private BaseCharacter GetRandomCharacter()
        {
            if (Random.Range(0.0f, 100.0f) <= _playerSpawnProbabilityPercent)
                return _playerPrefab;
            return _enemyPrefab;
        }
        private void Spowning(BaseCharacter characterPrefab)
        {
            _currentSpawnTimerSeconds += Time.deltaTime;
            if (_currentSpawnTimerSeconds > _currentSpawnIntervalSeconds)
            {
                _currentSpawnTimerSeconds = 0f;
                SetNewSpawnIntervalSeconds();


                var randomPointInsideRange = Random.insideUnitCircle * _range;
                var randomPosition = new Vector3(randomPointInsideRange.x, 1f, randomPointInsideRange.y) + transform.position;

                var character  = Instantiate(characterPrefab, randomPosition, Quaternion.identity, transform);

                if (LayerUtils.IsPlayer(characterPrefab.gameObject))
                {
                    _characterSpawnersController.ReportSpownPlayer();
                    
                }
                else 
                    _characterSpawnersController.ReportSpownEnemy();
            }
        }
    }
}