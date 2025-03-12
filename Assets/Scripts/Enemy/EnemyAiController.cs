using LearnGame.Enemy.States;
using UnityEngine;
using LearnGame.Boosters;
using LearnGame.Spawners;

namespace LearnGame.Enemy
{
    public class EnemyAiController : MonoBehaviour
    {
        [SerializeField]
        private float _viewRadius = 20f;
        [SerializeField]
        private float _minHpForEscapePercent = 20f;
        [SerializeField]
        private float _probabilityEscapePercent = 70f;

        private EnemyTarget _target;
        private EnemyStateMachine _stateMashine;

        protected void Awake()
        {
            var player = CharacterSpawnersController.instance.Player;
            var enemyDirectionController = GetComponent<EnemyDirectionController>();
            var powerUpController = GetComponent<PowerUpController>();
            var nawMesher = new NavMesher(transform);
            var enemyCharacter = GetComponent<EnemyCharacterView>();

            _target = new EnemyTarget(transform, player, _viewRadius, powerUpController);
            _stateMashine  = new EnemyStateMachine(enemyCharacter, enemyDirectionController, nawMesher, 
                _target, _minHpForEscapePercent, _probabilityEscapePercent);
        }

        protected void Update()
        {
            _target.FindClosest();
            _stateMashine.Update();
        }
    }
}