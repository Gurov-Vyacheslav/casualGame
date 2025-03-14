using LearnGame.Boosters;
using LearnGame.Enemy;
using LearnGame.Timer;
using UnityEngine;
using LearnGame.Spawners;
using LearnGame.Enemy.States;

namespace LearnGame.CompositionRoot
{
    [RequireComponent(typeof(EnemyAiControllerView))]
    public class EnemyAICompositionRoot: CompositionRoot<EnemyAiControllerView>
    {
        [SerializeField]
        private EnemyAIConfig _enemyAIConfig;

        private EnemyAiControllerView _view;

        public override EnemyAiControllerView Compose(ITimer timer)
        {
            _view = GetComponent<EnemyAiControllerView>();

            var powerUpController = GetComponent<PowerUpController>();
            var enemyTarget = new EnemyTargetGO(_enemyAIConfig, transform, CharacterSpawnersController.Instance.Player, powerUpController);

            var enemyCharacter = GetComponent<EnemyCharacterView>();
            var enemyDirectionController = GetComponent<EnemyDirectionController>();
            var nawMesher = new NavMesher(transform);
            var stateMachine =  new EnemyStateMachine(_enemyAIConfig, enemyCharacter, enemyDirectionController, nawMesher, enemyTarget);

            var enemyAIController = new EnemyAiControllerModel(_enemyAIConfig, enemyTarget, stateMachine);
            _view.Initialize(enemyAIController);

            return _view;
        }
    }
}
