using LearnGame.Enemy.States;
using UnityEngine;
using LearnGame.Movement;
using LearnGame.Shooting;

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
            var player = FindObjectOfType<PlayerCharacter>();
            var enemyDirectionController = GetComponent<EnemyDirectionController>();
            var enemyMovementController = GetComponent<CharacterMovementController>();
            var enemyShootingController = GetComponent<ShootingController>();
            var nawMesher = new NavMesher(transform);
            var enemyCharacter = GetComponent<EnemyCharacter>();

            _target = new EnemyTarget(transform, player, _viewRadius, enemyMovementController, enemyShootingController);
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