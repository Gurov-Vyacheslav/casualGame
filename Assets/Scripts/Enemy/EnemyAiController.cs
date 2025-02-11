using LearnGame.Enemy.States;
using UnityEngine;

namespace LearnGame.Enemy
{
    public class EnemyAiController : MonoBehaviour
    {
        [SerializeField]
        private float _viewRadius = 20f;

        private EnemyTarget _target;
        private EnemyStateMachine _stateMashine;
        protected void Awake()
        {
            var player = FindObjectOfType<PlayerCharacter>();
            var enemyDirectionController = GetComponent<EnemyDirectionController>();
            var nawMesher = new NavMesher(transform);

            _target = new EnemyTarget(transform, player, _viewRadius);
            _stateMashine  = new EnemyStateMachine(enemyDirectionController, nawMesher, _target);
        }
        protected void Update()
        {
            _target.FindClosest();
            _stateMashine.Update();
        }
    }
}