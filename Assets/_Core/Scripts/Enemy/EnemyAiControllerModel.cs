using LearnGame.FSM;

namespace LearnGame.Enemy
{
    public class EnemyAiControllerModel
    {
        private readonly IEnemyAIConfig _enemyAiConfig; 

        private IEnemyTarget _target;
        private BaseStateMachine _stateMashine;

        public EnemyAiControllerModel(IEnemyAIConfig enemyAiConfig, IEnemyTarget target, BaseStateMachine stateMashine)
        {
            _enemyAiConfig = enemyAiConfig;
            _target = target;
            _stateMashine = stateMashine;
        }

        public void FindClosestTarget() => _target.FindClosest();

        public void UpdateStateMashine() => _stateMashine.Update();
    }
}